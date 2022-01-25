using System;
using System.Text;

namespace Core
{
	public interface IDataLoader : IDisposable
    {
        /// <summary>
        /// Загрузить данные.
        /// </summary>
        /// <param name="validate">Проверить не загружая.</param>
        StringBuilder Run(bool validate);
    }
}
