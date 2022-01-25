using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace UserConverter
{
	class Test
	{
		public static object ParseINN(object value, IDataReader reader)
		{
			var text = PrepareMethods.GetEmptyStringIfNull(value, reader);
			
			if (!long.TryParse(text, out _))
				return string.Empty;
				
			return text;
		}
	}
}
