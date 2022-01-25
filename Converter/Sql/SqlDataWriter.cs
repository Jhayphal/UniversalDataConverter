using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Base;
using Core;

namespace Sql
{
	public class SqlDataWriter : IDataWriter
	{
		private const int MaxRowsCountInForOneInsertStatesment = 999;

		/// <summary>
		/// Имя таблицы.
		/// </summary>
		public string TableName { get; set; }

		/// <summary>
		/// Перезаписать.
		/// </summary>
		public bool Rewrite { get; set; }

		public StringBuilder Write(IDataReader reader, IReadOnlyCollection<ITrack> tracks, bool validate)
		{
			if (!reader.Read())
			{
				Console.WriteLine("No records.");

				return null;
			}

			// TODO: Добавить экранирование имён.
			var baseQuery = $"INSERT INTO dbo.[{ TableName }] (";

			bool firstTrack = true;

			foreach (var track in tracks)
			{
				if (track.Source == null)
					continue;

				string name = track.GetTextColumnName();

				baseQuery += (firstTrack ? string.Empty : ", ") + $"[{ name }]";

				firstTrack = false;
			}

			baseQuery += ") VALUES" + Environment.NewLine;

			StringBuilder query = new StringBuilder();

			if (Rewrite)
				query.Append(DropTable() + CreateTable(tracks));

			bool firstRow = false;
			int rowNumber = MaxRowsCountInForOneInsertStatesment;

			ReportRow row = null;

			do
			{
				if (rowNumber == MaxRowsCountInForOneInsertStatesment)
				{
					query.AppendLine();
					query.Append(baseQuery);

					rowNumber = 0;
					firstRow = true;
				}

				if (firstRow)
					query.Append("  (");
				else
					query.Append(", (");

				firstTrack = true;

				foreach (var track in tracks)
				{
					if (track.Source == null)
						continue;

					if (firstTrack)
						firstTrack = false;
					else
						query.Append(", ");

					var value = track.Source.GetValue(reader);

					if (value is string s)
						value = s.Replace("'", "''");

					try
					{
						query.Append(track.Destination.AsString(value));
					}
					catch (Exception e)
					{
						if (row == null)
						{
							int fieldsCount = tracks
								.Where(x => x.Source != null)
								.Count();

							object[] values = new object[fieldsCount];
							int i = -1;

							foreach(var item in tracks)
								if (item.Source != null)
									values[++i] = item.Source.GetValue(reader);

							row = new ReportRow(values);
						}

						string message = null;

						if (e is ArgumentNullException)
							message = $"Столбец '{ track.Destination.Name }' не допускает значений NULL.";

						else if (e is ArgumentOutOfRangeException)
							message = $"Значение '{ value }' столбца '{ track.Destination.Name }' выходит за пределы допустимого диапазона.";

						else if (e is ArgumentException)
							message = $"Значение '{ value }' столбца '{ track.Destination.Name }' имеет отличный от требуемого тип данных.";

						else
							message = $"Значение '{ value }' столбца '{ track.Destination.Name }' вызывает непредусмотренную ошибку.";

						if (validate)
							Report.Add(new ReportItem(row, message, ReportItem.MessageType.Error, DateTime.Now));
						else
							throw new InvalidOperationException(message + Environment.NewLine + " В строке: " + row.ToString());
					}
				}

				query.AppendLine(")");

				++rowNumber;
				firstRow = false;
				row = null;
			}
			while (reader.Read());

			query.Append("GO");

			foreach (var track in tracks)
			{
				if (track.Source == null)
					continue;

				track.AppendSelfToQuery(TableName, query);
			}

			return query;
		}

		private string DropTable()
		{
			return string.Format("IF OBJECT_ID(N'dbo.[{0}]', N'U') IS NOT NULL{1}\tDROP TABLE dbo.[{0}]{1}GO{1}{1}", TableName, Environment.NewLine);
		}

		private string CreateTable(IReadOnlyCollection<ITrack> tracks)
		{
			var query = new StringBuilder();
			query.AppendLine($"CREATE TABLE dbo.[{ TableName }] (");

			foreach (SqlTrack track in GetTracks(tracks))
			{
				var destination = track.SqlDestination;

				query.AppendLine($"\t[{ destination.Name }] { destination.CreateClause },");
			}

			query.AppendFormat("\t[ID_RECORD] BIGINT IDENTITY PRIMARY KEY{0}){0}GO{0}", Environment.NewLine);

			return query.ToString();
		}

		private IEnumerable<SqlTrack> GetTracks(IReadOnlyCollection<ITrack> tracks)
		{
			foreach(var track in tracks)
			{
				if (track is SqlTrack sqlTrack)
					foreach (var item in sqlTrack.GetTracks())
						yield return item;
				else
					throw new InvalidOperationException(track.GetType().ToString());
			}
		}
	}
}
