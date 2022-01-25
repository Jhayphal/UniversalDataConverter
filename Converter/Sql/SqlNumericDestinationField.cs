using System;

namespace Sql
{
	public class SqlNumericDestinationField : SqlDestinationField
    {
        public int Precision { get; set; }

        public int Scale { get; set; }

        public SqlNumericDestinationField()
        {
            // для сериализации
        }

        public SqlNumericDestinationField(string name, int precision, int scale = 0, bool allowNulls = false, decimal? defaultValue = null) : base(name, allowNulls, defaultValue)
        {
            Precision = precision;
            Scale = scale;
            CreateClause = $"NUMERIC({ precision }, { scale }) { (allowNulls ? string.Empty : "NOT ") }NULL{ (defaultValue.HasValue ? " DEFAULT(" + defaultValue.ToString() + ")" : string.Empty) }";
        }

        public override void Validate(object value)
        {
            base.Validate(value);

            if (value == null)
                return;

            string text = value
                .ToString()
                .Trim()
                .Replace(" ", string.Empty) // удалим пробелы внутри
                .TrimStart('-', '+'); // знак нас не интересует

            if (Scale == 0)
            {
                if (!long.TryParse(text, out var _))
                    throw new ArgumentException();

                if (Precision < text.Length)
                    throw new ArgumentOutOfRangeException();
            }
            else
            {
                if (!double.TryParse(text, out var _))
                    throw new ArgumentException();

                string[] parts = text.Split('.', ',');

                if (parts[0].Length > Precision)
                    throw new ArgumentOutOfRangeException();

                if (parts.Length > 1 && parts[1].Length > Scale)
                    throw new ArgumentOutOfRangeException();
            }
        }

		public override SqlDestinationField CloneWithName(string name)
		{
			return new SqlNumericDestinationField(name: name, precision: Precision, scale: Scale, allowNulls: AllowNulls, defaultValue: (decimal?)DefaultValue);
		}

		public override string AsString(object value)
		{
			return base.AsString(value).Replace(',', '.');
		}
	}
}
