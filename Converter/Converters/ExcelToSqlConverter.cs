using Core;
using System.Collections.Generic;
using System.Data;

namespace Converters
{
	public abstract class ExcelToSqlConverter
    {
        protected abstract IDataReader GetDatasource();

        protected abstract IReadOnlyCollection<ITrack> GetTracks();

        public abstract void Run(string tableName, bool rewrite, bool validate, string resultFileName);
    }
}
