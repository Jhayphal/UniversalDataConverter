using System;
using System.Data;

namespace Excel
{
    public static class ExcelFactory
    {
		public static IDataReader Create(params string[] fileNames)
		{
			if (fileNames == null)
				throw new ArgumentNullException();

			if (fileNames.Length == 0)
				throw new ArgumentException("Нет файлов в списке.");

			var result = new DataReadersChain();

			foreach (var fileName in fileNames)
				result.AddNext(new ExcelReader(fileName));

			return result;
		}
    }
}
