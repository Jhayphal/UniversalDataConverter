using System;
using System.Text;

namespace Base
{
	public class ReportRow
    {
        public readonly object[] Values;

        public ReportRow(object[] values)
        {
            Values = values ?? throw new ArgumentNullException();
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            foreach (var value in Values)
            {
                if (value != null && builder.Length > 0)
                    builder.Append('\t');

                builder.Append(value);
            }

            return builder.ToString();
        }
    }
}
