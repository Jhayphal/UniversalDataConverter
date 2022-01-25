using Core;
using System;
using System.Data;

namespace Base
{
    public class SourceField : ISourceField
    {
        public string Name { get; set; }

        /// <summary>
        /// Функция для предварительной обработки значений.
        /// </summary>
        public Func<object, IDataReader, object> Prepare { get; set; }

        public SourceField()
        {
            // для сериализации
        }

        public SourceField(string name, Func<object, IDataReader, object> prepare = null)
        {
            Name = name ?? throw new ArgumentNullException();
            Prepare = prepare;
        }

        public virtual object GetValue(IDataReader reader)
        {
            var value = reader[Name];

            if (Prepare == null)
                return value;

            return Prepare(value, reader);
        }

        public override string ToString()
        {
            return Name;
        }
    }

	public class VirtualStaticField : ISourceField
	{
		public string Name { get; set; }

		public object Value { get; set; }

		public VirtualStaticField()
		{
			// для сериализации
		}

		public VirtualStaticField(string name, object value)
		{
			Name = name ?? throw new ArgumentNullException();
			Value = value;
		}

		public virtual object GetValue(IDataReader reader)
		{
			return Value;
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
