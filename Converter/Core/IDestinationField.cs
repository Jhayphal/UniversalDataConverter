using System;

namespace Core
{
	public interface IDestinationField
    {
        /// <summary>
        /// Имя поля в приемнике.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Проверка значения на корректность.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <exception cref="ArgumentException">Тип значения несовместим с типом поля.</exception>
        /// <exception cref="ArgumentNullException">Поле не допускает значений null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Значение не подходит под ограничения типа.</exception>
        void Validate(object value);
        
        /// <summary>
        /// Преобразовать значение в строку в случае прохождения проверки.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <remarks>Вызывает метод Validate перед выполнением операций, который пробрасывает исключения в случае отказа. См. документацию к методу Validate.</remarks>
        /// <returns>Строковое представление значения.</returns>
        string AsString(object value);
    }
}
