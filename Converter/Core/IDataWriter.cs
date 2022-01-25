using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Core
{
    public interface IDataWriter
    {
        /// <summary>
        /// Записать данные.
        /// </summary>
        /// <param name="reader">Источник данных.</param>
        /// <param name="tracks">Маршруты.</param>
        /// <param name="validate">Проверить без записи.</param>
        StringBuilder Write(IDataReader reader, IReadOnlyCollection<ITrack> tracks, bool validate);
    }
}