using System.Data;

namespace Core
{
	public interface ISourceField
    {
        /// <summary>
        /// Имя поля в источнике данных.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Получить значение поля.
        /// </summary>
        /// <param name="reader">Истоник данных.</param>
        /// <returns>Значение поля.</returns>
        object GetValue(IDataReader reader);
    }
}
