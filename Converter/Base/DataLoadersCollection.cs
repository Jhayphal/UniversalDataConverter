using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Base
{
	public class DataLoadersCollection : ICollection<IDataLoader>, IDisposable
	{
		public int Count => _loaders.Count;

		public bool IsReadOnly => false;

		List<IDataLoader> _loaders;

		public DataLoadersCollection()
		{
			_loaders = new List<IDataLoader>();
		}

		/// <summary>
		/// Выполняет все загрузчики данных.
		/// </summary>
		/// <param name="validate">Проверить без загрузки.</param>
		public bool Run(TextWriter result, bool validate)
		{
			foreach (var item in _loaders)
			{
				result.WriteLine(item.Run(validate));
				result.WriteLine();

				item.Dispose();
			}

			return !Report.HasItems;
		}

		/// <summary>
		/// Меняет местами элементы в коллекции.
		/// </summary>
		/// <param name="item1">Загрузчик данных.</param>
		/// <param name="item2">Загрузчик данных.</param>
		public void Swap(IDataLoader item1, IDataLoader item2)
		{
			throw new NotImplementedException("Не реализован.");
		}

		public void Add(IDataLoader item)
		{
			_loaders.Add(item);
		}

		public void Clear()
		{
			_loaders.Clear();
		}

		public bool Contains(IDataLoader item)
		{
			return _loaders.Contains(item);
		}

		public void CopyTo(IDataLoader[] array, int arrayIndex)
		{
			_loaders.CopyTo(array, arrayIndex);
		}

		public IEnumerator<IDataLoader> GetEnumerator()
		{
			return _loaders.GetEnumerator();
		}

		public bool Remove(IDataLoader item)
		{
			return _loaders.Remove(item);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _loaders.GetEnumerator();
		}

		public void Dispose()
		{
			if (_loaders == null)
				return;

			foreach (var item in _loaders)
				item.Dispose();

			_loaders = null;
		}
	}
}
