using NDbfReader;
using System;
using System.Linq;
using System.Text;

namespace Dbf
{
	public sealed class PropertyProxyReader : Reader
	{
		public PropertyProxyReader(Table table, Encoding encoding) : base(table, encoding) { }

		public object this[string name] => GetValue(name);

		public string GetName(int i)
		{
			return Header.Columns[i].Name;
		}

		public int GetOrdinal(string name)
		{
			int index = 0;

			while (!string.Equals(name, GetName(index++))) ;

			return index - 1;
		}

		public object GetValue(int i)
		{
			return GetValue(GetName(i));
		}

		public DateTime GetDateTime(int i)
		{
			return GetDateTime(GetName(i)) ?? DateTime.MinValue;
		}

		public bool GetBoolean(int i)
		{
			return GetBoolean(GetName(i)) ?? false;
		}

		public byte GetByte(int i)
		{
			return (byte)GetValue(i);
		}

		public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			var bytes = (byte[])GetValue(i);
			var count = Math.Min(length, bytes.Length);

			Array.Copy(bytes, fieldOffset, buffer, bufferoffset, count);

			return count;
		}

		public char GetChar(int i)
		{
			return (char)GetValue(i);
		}

		public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
		{
			var bytes = (char[])GetValue(i);
			var count = Math.Min(length, bytes.Length);

			Array.Copy(bytes, fieldoffset, buffer, bufferoffset, count);

			return count;
		}

		public string GetDataTypeName(int i)
		{
			return GetValue(i).GetType().Name;
		}

		public decimal GetDecimal(int i)
		{
			return GetDecimal(GetName(i)) ?? decimal.Zero;
		}

		public double GetDouble(int i)
		{
			return (double)GetValue(i);
		}

		public Type GetFieldType(int i)
		{
			return GetValue(i).GetType();
		}

		public float GetFloat(int i)
		{
			return (float)GetValue(i);
		}

		public Guid GetGuid(int i)
		{
			return (Guid)GetValue(i);
		}

		public short GetInt16(int i)
		{
			return (short)GetValue(i);
		}

		public int GetInt32(int i)
		{
			return GetInt32(GetName(i));
		}

		public long GetInt64(int i)
		{
			return (long)GetValue(i);
		}

		public string GetString(int i)
		{
			return GetString(GetName(i));
		}

		public int GetValues(object[] values)
		{
			values = Header.Columns
				.Select(x => GetValue(x))
				.ToArray();

			return values.Length;
		}

		public bool IsDBNull(int i)
		{
			return GetValue(i) == DBNull.Value;
		}

		public int FieldCount => Header.Columns.Count;
	}
}
