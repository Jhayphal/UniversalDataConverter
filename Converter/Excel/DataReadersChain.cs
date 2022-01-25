using System;
using System.Collections.Generic;
using System.Data;

namespace Excel
{
	internal class DataReadersChain : IDataReader
    {
        public object this[int i] => _reader[i];

        public object this[string name] => _reader[name];

        public int Depth => _reader.Depth;

        public bool IsClosed => _reader.IsClosed;

        public int RecordsAffected => _reader.RecordsAffected;

        public int FieldCount => _reader.FieldCount;

        private IDataReader _reader;
        private List<IDataReader> _readers;
        private int _index;

        public DataReadersChain()
        {
            _readers = new List<IDataReader>();
            _index = 0;
        }

        public DataReadersChain(IDataReader reader) : this()
        {
            AddNext(reader);
        }

        public DataReadersChain(IReadOnlyList<IDataReader> readers) : this()
        {
            AddNext(readers);
        }

        public void AddNext(IDataReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException();

            if (_reader == null)
                _reader = reader;

            _readers.Add(reader);
        }

        public void AddNext(IReadOnlyList<IDataReader> readers)
        {
            if (readers == null)
                throw new ArgumentNullException();

            foreach (var item in readers)
                AddNext(item);
        }

        public void Close()
        {
            _reader.Close();
        }

        public void Dispose()
        {
            if (_reader != null)
                _reader = null;

            if (_readers != null)
            {
                foreach (var item in _readers)
                    item.Dispose();

                _readers = null;
            }
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
            return _reader.GetName(i);
        }

        public int GetOrdinal(string name)
        {
            return _reader.GetOrdinal(name);
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
            return _reader.NextResult();
        }

        public bool Read()
        {
            var result = _reader.Read();

            if (!result)
            {
                if (++_index < _readers.Count)
                {
                    _reader = _readers[_index];

                    return Read();
                }
            }

            return result;
        }
    }
}
