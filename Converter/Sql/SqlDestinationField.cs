using System;
using Core;

namespace Sql
{
    public abstract class SqlDestinationField : IDestinationField
    {
        public string Name { get; set; }

        /// <summary>
        /// Строка форматирования.
        /// </summary>
        public string FormatString { get; set; }

        /// <summary>
        /// Выражение для создания столбца.
        /// </summary>
        public string CreateClause { get; set; }

        /// <summary>
        /// Допускает значение NULL.
        /// </summary>
        public bool AllowNulls { get; set; }

        /// <summary>
        /// Стандартное значение.
        /// </summary>
        public object DefaultValue { get; set; }

        public SqlDestinationField()
        {
            FormatString = "{0}";
        }

        public SqlDestinationField(string name, bool allowNulls, object defaultValue) : this()
        {
            Name = name ?? throw new ArgumentNullException();
            AllowNulls = allowNulls;
            DefaultValue = defaultValue;
        }

		public abstract SqlDestinationField CloneWithName(string name);

        public virtual void Validate(object value)
        {
            if (value == null)
            {
                if (!AllowNulls)
                    throw new ArgumentNullException();

                return;
            }
        }

        public virtual string AsString(object value)
        {
            Validate(value);

            if (value == null)
            {
                if (AllowNulls || DefaultValue == null)
                    return "NULL";

                return string.Format(FormatString, DefaultValue);
            }

            return string.Format(FormatString, value);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
