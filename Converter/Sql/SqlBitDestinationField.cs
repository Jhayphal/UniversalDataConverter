using System;
using System.Collections.Generic;

namespace Sql
{
	public class SqlBitDestinationField : SqlDestinationField
    {
        private static List<string> TrueStates;
        private static List<string> FalseStates;

        static SqlBitDestinationField()
        {
            TrueStates = new List<string>
            {
                "1", "TRUE", "YES", "Y", "ДА", "Д", "ИСТИНА"
            };

            FalseStates = new List<string>
            {
                "0", "FALSE", "NO", "N", "НЕТ", "Н", "ЛОЖЬ"
            };
        }

        public SqlBitDestinationField()
        {
            // для сериализации
        }

        public SqlBitDestinationField(string name, bool allowNulls = false, bool? defaultValue = null) : base(name, allowNulls, defaultValue)
        {
            CreateClause = $"BIT { (allowNulls ? string.Empty : "NOT ") }NULL{ (defaultValue.HasValue ? " DEFAULT(" + (defaultValue.Value ? "1" : "0") + ")" : string.Empty) }";
        }

        public override string AsString(object value)
        {
            Validate(value);

            if (value == null)
            {
                if (AllowNulls || DefaultValue == null)
                    return "NULL";

                return string.Format(FormatString, DefaultValue);
            }

            string text = value
                .ToString()
                .Trim()
                .ToUpper();

            if (TrueStates.Contains(text))
                value = 1;

            else
                value = 0;

            return string.Format(FormatString, value);
        }

		public override SqlDestinationField CloneWithName(string name)
		{
			return new SqlBitDestinationField(name: name, allowNulls: AllowNulls, defaultValue: (bool?)DefaultValue);
		}

		public override void Validate(object value)
        {
            base.Validate(value);

            if (value == null)
                return;

            string text = value
                .ToString()
                .Trim()
                .ToUpper();

            if (TrueStates.Contains(text) || FalseStates.Contains(text))
                return;

            throw new ArgumentOutOfRangeException();
        }
    }
}
