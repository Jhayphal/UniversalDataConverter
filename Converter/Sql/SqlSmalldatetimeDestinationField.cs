using System;

namespace Sql
{
	public class SqlSmalldatetimeDestinationField : SqlDestinationField
    {
        public SqlSmalldatetimeDestinationField()
        {
            // для сериализации
        }

        public SqlSmalldatetimeDestinationField(string name, bool allowNulls = true, DateTime? defaultValue = null) : base(name, allowNulls, defaultValue)
        {
            CreateClause = $"SMALLDATETIME { (allowNulls ? string.Empty : "NOT ") }NULL{ (defaultValue.HasValue ? " DEFAULT('" + defaultValue.Value.ToString("s") + "')" : string.Empty) }";
            FormatString = "'{0:s}'";
        }

		public override SqlDestinationField CloneWithName(string name)
		{
			if (DefaultValue == null)
				return new SqlSmalldatetimeDestinationField(name: name, allowNulls: AllowNulls, defaultValue: null);

			return new SqlSmalldatetimeDestinationField(name: name, allowNulls: AllowNulls, defaultValue: (DateTime?)DefaultValue);
		}

		public override void Validate(object value)
        {
            base.Validate(value);

            if (value == null)
                return;

            string text = value
                .ToString()
                .Trim();

            if (!DateTime.TryParse(text, out var date))
                throw new ArgumentException();

            int year = date.Year;

            if (year < 1900 || year > 2100)
                throw new ArgumentOutOfRangeException();
        }
    }
}
