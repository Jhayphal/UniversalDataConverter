using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Base
{
	public static class Report
    {
        private static List<ReportItem> _data = new List<ReportItem>();
        private static List<ReportRow> _rows = new List<ReportRow>();

        public static bool HasItems => _rows.Count > 0;

        public static void Add(ReportItem item)
        {
            _rows.Add(item.Row);
            _data.Add(item);
        }

        public static void Clear()
        {
            _rows.Clear();
            _data.Clear();
        }

        public static string Read()
        {
            StringBuilder builder = new StringBuilder();

            foreach(var row in _rows)
            {
                builder.AppendLine(row.ToString());

                var items = _data.Where(x => object.ReferenceEquals(x.Row, row));

                foreach(var item in items)
                    builder.AppendLine(item.ToString());

                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}
