using NDbfReader;
using System;
using System.Data;
using System.Text;

namespace Dbf
{
	public sealed class DbfDataReader : IDataReader, IDisposable
	{
		private Table table;
		private readonly PropertyProxyReader reader;

		public object this[int i] => reader.GetValue(i);

		public object this[string name] => reader[name];

		public int Depth => 0;

		public bool IsClosed => table == null;

		public int RecordsAffected => 0;

		public int FieldCount => reader.FieldCount;

		public DbfDataReader(string fileName)
			: this(fileName, Encoding.GetEncoding(1251)) { }

		public DbfDataReader(string fileName, Encoding encoding)
		{
			table = Table.Open(fileName);
			reader = new PropertyProxyReader(table, encoding);
		}

		public void Close()
		{
			Dispose();
		}

		public void Dispose()
		{
			table.Dispose();

			table = null;
		}

		public bool GetBoolean(int i)
		{
			return reader.GetBoolean(i);
		}

		public byte GetByte(int i)
		{
			return reader.GetByte(i);
		}

		public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			return reader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
		}

		public char GetChar(int i)
		{
			return reader.GetChar(i);
		}

		public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
		{
			return GetChars(i, fieldoffset, buffer, bufferoffset, length);
		}

		public IDataReader GetData(int i)
		{
			throw new NotImplementedException();
		}

		public string GetDataTypeName(int i)
		{
			return reader.GetDataTypeName(i);
		}

		public DateTime GetDateTime(int i)
		{
			return reader.GetDateTime(i);
		}

		public decimal GetDecimal(int i)
		{
			return reader.GetDecimal(i);
		}

		public double GetDouble(int i)
		{
			return reader.GetDouble(i);
		}

		public Type GetFieldType(int i)
		{
			return reader.GetFieldType(i);
		}

		public float GetFloat(int i)
		{
			return reader.GetFloat(i);
		}

		public Guid GetGuid(int i)
		{
			return reader.GetGuid(i);
		}

		public short GetInt16(int i)
		{
			return reader.GetInt16(i);
		}

		public int GetInt32(int i)
		{
			return reader.GetInt32(i);
		}

		public long GetInt64(int i)
		{
			return reader.GetInt64(i);
		}

		public string GetName(int i)
		{
			return reader.GetName(i);
		}

		public int GetOrdinal(string name)
		{
			return reader.GetOrdinal(name);
		}

		public DataTable GetSchemaTable()
		{
			throw new NotImplementedException();
		}

		public string GetString(int i)
		{
			return reader.GetString(i);
		}

		public object GetValue(int i)
		{
			return reader.GetValue(i);
		}

		public int GetValues(object[] values)
		{
			return reader.GetValues(values);
		}

		public bool IsDBNull(int i)
		{
			return reader.IsDBNull(i);
		}

		public bool NextResult()
		{
			return false;
		}

		public bool Read()
		{
			return reader.Read();
		}
	}
}
