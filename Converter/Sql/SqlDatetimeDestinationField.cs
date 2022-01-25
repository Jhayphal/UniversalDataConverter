using System;

namespace Sql
{
	public class SqlDatetimeDestinationField : SqlDestinationField
    {
        public SqlDatetimeDestinationField()
        {
            // для сериализации
        }

        public SqlDatetimeDestinationField(string name, bool allowNulls = true, DateTime? defaultValue = null) : base(name, allowNulls, defaultValue)
        {
            CreateClause = $"DATETIME { (allowNulls ? string.Empty : "NOT ") }NULL{ (defaultValue.HasValue ? " DEFAULT('" + defaultValue.Value.ToString("s") + "')" : string.Empty) }";
            FormatString = "'{0:s}'";
        }

		public override SqlDestinationField CloneWithName(string name)
		{
			return new SqlDatetimeDestinationField(name: name, allowNulls: AllowNulls, defaultValue: (DateTime?)DefaultValue);
		}

		public override void Validate(object value)
        {
            base.Validate(value);

            if (value == null)
                return;

            string text = value
                .ToString()
                .Trim();

            if (!DateTime.TryParse(text, out var _))
                throw new ArgumentException();
        }
    }
}
