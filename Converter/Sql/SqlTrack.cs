using Base;
using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sql
{
    public class SqlTrack : ITrack
    {
        public ISourceField Source { get; set; }

		public SourceField SourceField { get; set; }

        public IDestinationField Destination
        {
            get => SqlDestination;
            set => SqlDestination = value as SqlDestinationField;
        }

        public SqlDestinationField SqlDestination { get; set; }

        public void Validate()
        {
            if (Destination == null || string.IsNullOrWhiteSpace(Destination.Name))
                throw new NullReferenceException("Не указана конечная точка маршрута.");

            if (Source == null)
            {
                if (!SqlDestination.AllowNulls && SqlDestination.DefaultValue == null)
                    throw new InvalidCastException($"Поле [{Destination.Name}], которое не допускает значений NULL не имеет источника данных и значения по умолчанию.");
            }
            else if (string.IsNullOrWhiteSpace(Source.Name))
                throw new InvalidOperationException("Не указано имя поля в источнике.");
        }

		/// <summary>
		/// Для геренации нескольких дорожек из одной.
		/// </summary>
		/// <returns>Маршруты.</returns>
		/// <remarks>Например, для заполнения справочников (см. SqlDictionaryTrack в ComInTec).</remarks>
		public virtual IEnumerable<SqlTrack> GetTracks()
		{
			yield return this;
		}

		public virtual string GetTextColumnName()
		{
			return Destination.Name;
		}

		public virtual void AppendSelfToQuery(string dataTableName, StringBuilder query)
		{
			// реализовать в случае необходимости
			// используется в справочниках
		}
	}
}
