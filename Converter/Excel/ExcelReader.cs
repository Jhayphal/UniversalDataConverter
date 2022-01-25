using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Excel
{
	/// <summary>
	/// Предназначен для совместного освобождения ресурсов.
	/// </summary>
	public class ExcelReader : IDataReader
	{
		public object this[int i] => _reader[i];

		public object this[string name]
		{
			get
			{
				if (_columnNames == null)
					throw new NotSupportedException();

				if (_columnNames.ContainsKey(name))
				{
					return this.GetValue(_columnNames[name]);
				}

				throw new IndexOutOfRangeException($"Имя колонки {name} не существует.");
			}
		}

		public int Depth => _reader.Depth;

		public bool IsClosed => _reader.IsClosed;

		public int RecordsAffected => _reader.RecordsAffected;

		public int FieldCount => _reader.FieldCount;


		private readonly string _fileName;
		private IDataReader _reader;
		private FileStream _file;

		private Dictionary<string, int> _columnNames;

		public ExcelReader(string fileName)
		{
			if (string.IsNullOrWhiteSpace(fileName))
				throw new ArgumentException("Не указано имя файла.");

			if (!File.Exists(fileName))
				throw new FileNotFoundException(fileName);

			_fileName = fileName;
		}

		/// <summary>
		/// Read column names from next row.
		/// </summary>
		void ReadHeader()
		{
			_columnNames = new Dictionary<string, int>();

			int count = this.FieldCount;

			for (int i = 0; i < count; ++i)
			{
				string columnName = this.GetString(i);

				if (string.IsNullOrWhiteSpace(columnName))
					continue;

				if (_columnNames.ContainsKey(columnName))
					throw new InvalidOperationException("Имена колонок должны быть уникальны. Дублируется имя '" + columnName + "'.");

				_columnNames.Add(columnName, i);
			}
		}

		public void Close()
		{
			_reader.Close();
			_file.Close();
		}

		public void Dispose()
		{
			_reader?.Dispose();
			_file?.Dispose();
		}

		public bool GetBoolean(int i)
		{
			return _reader.GetBoolean(i);
		}

		public byte GetByte(int i)
		{
			return _reader.GetByte(i);
		}

		public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			return _reader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
		}

		public char GetChar(int i)
		{
			return _reader.GetChar(i);
		}

		public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
		{
			return _reader.GetChars(i, fieldoffset, buffer, bufferoffset, length);
		}

		public IDataReader GetData(int i)
		{
			return _reader.GetData(i);
		}

		public string GetDataTypeName(int i)
		{
			return _reader.GetDataTypeName(i);
		}

		public DateTime GetDateTime(int i)
		{
			return _reader.GetDateTime(i);
		}

		public decimal GetDecimal(int i)
		{
			return _reader.GetDecimal(i);
		}

		public double GetDouble(int i)
		{
			return _reader.GetDouble(i);
		}

		public Type GetFieldType(int i)
		{
			return _reader.GetFieldType(i);
		}

		public float GetFloat(int i)
		{
			return _reader.GetFloat(i);
		}

		public Guid GetGuid(int i)
		{
			return _reader.GetGuid(i);
		}

		public short GetInt16(int i)
		{
			return _reader.GetInt16(i);
		}

		public int GetInt32(int i)
		{
			return _reader.GetInt32(i);
		}

		public long GetInt64(int i)
		{
			return _reader.GetInt64(i);
		}

		public string GetName(int i)
		{
			if (_columnNames == null)
				throw new NotSupportedException();

			if (i >= this.FieldCount)
				throw new IndexOutOfRangeException();

			foreach (string name in _columnNames.Keys)
			{
				if (_columnNames[name] == i)
					return name;
			}

			return null;
		}

		public int GetOrdinal(string name)
		{
			if (_columnNames == null)
				throw new NotSupportedException();

			return _columnNames[name];
		}

		public DataTable GetSchemaTable()
		{
			return _reader.GetSchemaTable();
		}

		public string GetString(int i)
		{
			return _reader.GetString(i);
		}

		public object GetValue(int i)
		{
			return _reader.GetValue(i);
		}

		public int GetValues(object[] values)
		{
			return _reader.GetValues(values);
		}

		public bool IsDBNull(int i)
		{
			return _reader.IsDBNull(i);
		}

		public bool NextResult()
		{
			if (_columnNames != null)
				_columnNames = null;

			return _reader.NextResult();
		}

		public bool Read()
		{
			try
			{
				bool notInitialized = _reader == null;

				if (notInitialized)
				{
					_file = new FileStream(_fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
					_reader = ExcelDataReader.ExcelReaderFactory.CreateReader(_file);
				}

				bool result = _reader.Read();

				if (notInitialized && result)
				{
					ReadHeader();

					result = Read();
				}

				return result;
			}
			catch(Exception e)
			{
				throw new IOException(_fileName, e);
			}
		}
	}
}
