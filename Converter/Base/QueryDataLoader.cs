using Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Base
{
	public class QueryDataLoader : IDataLoader
	{
		private StringBuilder data = new StringBuilder();

		public string Query 
		{ 
			get => data.ToString(); 
			set 
			{
				data.Clear();

				if (value != null)
					data.Append(value); 
			}
		}

		public QueryDataLoader()
		{

		}

		public QueryDataLoader(string query)
		{
			data.Append(query);
		}

		public QueryDataLoader(StringBuilder query)
		{
			data.Append(query);
		}

		public void Dispose()
		{
			data.Clear();
		}

		public StringBuilder Run(bool validate)
		{
			return data;
		}

		/// <summary>
		/// Создать загрузчик из sql-запроса.
		/// </summary>
		/// <param name="fileName">Имя файла sql.</param>
		/// <param name="tableName">Имя таблицы. Если указанно, заменяет все вхождения строки {0} в запросе на это имя.</param>
		/// <returns>Загрузчик sql.</returns>
		public static QueryDataLoader FromFile(string fileName, string tableName = null)
		{
			using (var script = new StreamReader(fileName))
			{
				StringBuilder builder = new StringBuilder();

				if (!string.IsNullOrWhiteSpace(tableName))
					builder.AppendFormat(script.ReadToEnd(), tableName);
				else
					builder.Append(script.ReadToEnd());

				return new QueryDataLoader(builder);
			}
		}

		public static QueryDataLoader Merge(string targetTableName, QueryDataLoader source, Dictionary<string, string> key, Dictionary<string, string> fields)
		{
			return Merge(targetTableName, source.Query, key, fields);
		}

		public static QueryDataLoader Merge(string targetTableName, string source, Dictionary<string, string> key, Dictionary<string, string> fields)
		{
			if (key == null || key.Count == 0)
				throw new KeyNotFoundException(nameof(key));

			if (fields != null && fields.Count == 0)
				fields = null;

			if (fields != null)
				foreach (var field in fields.Keys)
					if (key.ContainsKey(field))
						throw new InvalidOperationException($"Поле ({field}) не может присутствовать и в ключе и в списке полей одновремено.");

			var sourceText = source.Trim();

			if (sourceText.Contains(" "))
				sourceText = "(" + sourceText + ")";
			else
			{
				if (sourceText.StartsWith("dbo.[", StringComparison.OrdinalIgnoreCase))
				{
					if (!sourceText.EndsWith("]"))
						sourceText += "]";
				}
				else
					sourceText = "dbo.[" + source + "]";
			}

			var builder = new StringBuilder();
			builder.AppendFormat(";MERGE dbo.[{0}] AS Target", targetTableName);
			builder.AppendLine();
			builder.AppendFormat("USING {0} AS Source", sourceText);
			builder.AppendLine();
			builder.Append("	ON ");

			bool first = true;

			foreach (var pair in key)
			{
				if (!first)
					builder.Append(" AND ");

				builder.AppendFormat("Target.[{0}] = {1}", pair.Key, PrepareMergeExpression(pair.Value));

				first = false;
			}

			if (fields != null)
			{
				builder.AppendLine();

				builder.AppendLine("WHEN MATCHED THEN");
				builder.AppendLine("	UPDATE SET");

				first = true;

				foreach (var pair in fields)
				{
					if (!first)
						builder.AppendLine(",");

					builder.AppendFormat("\t\t[{0}] = {1}", pair.Key, PrepareMergeExpression(pair.Value));

					first = false;
				}
			}

			builder.AppendLine();
			builder.AppendLine("WHEN NOT MATCHED THEN");
			builder.Append("\tINSERT (");

			first = true;

			foreach (var pair in key)
			{
				if (!first)
					builder.Append(", ");

				builder.AppendFormat("[{0}]", pair.Key);

				first = false;
			}

			if (fields != null)
				foreach (var pair in fields)
					builder.AppendFormat(", [{0}]", pair.Key);

			builder.AppendLine(")");
			builder.AppendLine("\tVALUES (");

			first = true;

			foreach (var pair in key)
			{
				if (!first)
					builder.AppendLine(",");

				builder.AppendFormat("\t\t{0}", PrepareMergeExpression(pair.Value));

				first = false;
			}

			if (fields != null)
				foreach (var pair in fields)
				{
					builder.AppendLine(",");
					builder.AppendFormat("\t\t{0}", PrepareMergeExpression(pair.Value));
				}

			builder.AppendLine();
			builder.AppendLine("\t);");
			builder.Append("GO");

			return new QueryDataLoader(builder);
		}

		private static string PrepareMergeExpression(string expression)
		{
			if (string.IsNullOrWhiteSpace(expression))
				throw new ArgumentException(nameof(expression));

			expression = expression.Trim();

			if (expression.Contains(" ")
				|| expression.Contains("-")
				|| expression.Contains(".")
				|| expression.Contains(",")
				|| int.TryParse(expression, out var _))
				return expression;

			if (expression.StartsWith("["))
				expression = expression.Trim('[', ']');

			return "Source.[" + expression + "]";
		}
	}
}
