using System;

namespace Sql
{
	public class SqlCharDestinationField : SqlDestinationField
	{
		public int Precision { get; set; }

		public SqlCharDestinationField()
		{
			// для сериализации
		}

		public SqlCharDestinationField(string name, int precision, bool allowNulls = false, string defaultValue = null) : base(name, allowNulls, defaultValue)
		{
			CreateClause = $"CHAR({ precision }) { (allowNulls ? string.Empty : "NOT ") }NULL{ (defaultValue != null ? " DEFAULT(" + defaultValue.ToString() + ")" : string.Empty) }";
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
			return new SqlCharDestinationField(name: name, precision: Precision, allowNulls: AllowNulls, defaultValue: DefaultValue?.ToString());
		}
	}
}
