using System.Text;

namespace Core
{
	/// <summary>
	/// Маршрут передачи значений.
	/// </summary>
	public interface ITrack
    {
        /// <summary>
        /// Поле-источник.
        /// </summary>
        ISourceField Source { get; set; }

        /// <summary>
        /// Поле-назначение.
        /// </summary>
        IDestinationField Destination { get; set; }

        /// <summary>
        /// Проверка корректности заполнения.
        /// </summary>
        void Validate();

		string GetTextColumnName();

		void AppendSelfToQuery(string dataTableName, StringBuilder query);
	}
}
