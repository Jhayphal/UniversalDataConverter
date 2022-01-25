using System;

namespace Sql
{
	public class SqlNvarcharDestinationField : SqlDestinationField
    {
        public int Precision { get; set; }

        public SqlNvarcharDestinationField()
        {
            // для сериализации
        }

        public SqlNvarcharDestinationField(string name, int precision, bool allowNulls = false, string defaultValue = null) : base(name, allowNulls, defaultValue)
        {
            CreateClause = $"NVARCHAR({ precision }) { (allowNulls ? string.Empty : "NOT ") }NULL{ (defaultValue != null ? " DEFAULT(" + defaultValue.ToString() + ")" : string.Empty) }";
            FormatString = "'{0}'";
            Precision = precision;
        }

        public override void Validate(object value)
        {
            base.Validate(value);

            if (value == null)
                return;

            string text = value
                .ToString()
                .TrimEnd();

            if (text.Length > Precision)
                throw new ArgumentOutOfRangeException();
        }

		public override SqlDestinationField CloneWithName(string name)
		{
			return new SqlNvarcharDestinationField(name: name, precision: Precision, allowNulls: AllowNulls, defaultValue: DefaultValue.ToString());
		}
	}
}
