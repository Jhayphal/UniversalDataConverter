using System;
using System.Data;

namespace Base
{
	public static class PrepareMethods
	{
		public static object GetEmptyStringIfNull(object value, IDataReader reader)
		{
			if (value == null)
				return string.Empty;

			if (value is string s)
				return s.Trim();

			return value;
		}

		public static object GetZeroIfNullOrEmpty(object value, IDataReader reader)
		{
			if (value == null)
				return 0;

			if (value is string s)
				if (string.IsNullOrWhiteSpace(s))
					return 0;

			return value;
		}

		public static object GetSmalldatetime(object value, IDataReader reader)
		{
			if (value == null)
				return null;

			else if (value is DateTime d)
				return d.Year < 1900 || d.Year > 2100 ? null : value;

			else if (value is string s)
			{
				if (DateTime.TryParse(s, out var result))
					return result;

				return null;
			}

			return value;
		}

		public static object GetInteger(object o, IDataReader reader)
		{
			if (o == null)
				return 0;

			return GetInt32(o, reader);
		}

		public static object GetDouble(object o, IDataReader reader)
		{
			if (o == null)
				return 0d;

			if (o is double d)
				return d;

			if (o is float f)
				return (double)f;

			if (o is string s)
				return double.Parse(s); // намеренно с выбросом исключения

			if (o is int i)
				return (double)i;

			if (o is decimal dc)
				return (double)dc;

			return 0d;
		}

		static int? GetInt32(object o, IDataReader reader)
		{
			if (o is int i)
				return i;

			if (o is long l)
				return (int)l;

			if (o is short s)
				return s;

			if (o is float f)
				return (int)f;

			if (o is double d)
				return (int)d;

			if (o is byte b)
				return b;

			if (o is string && int.TryParse(o.ToString(), out int x))
				return x;

			return null;
		}

		public static object GetLogic(object value, IDataReader reader)
		{
			if (value == null)
				return 0;

			if (value is bool b)
				return b ? 1 : 0;

			var logic = GetInteger(value, reader);

			if (logic != null)
			{
				switch (logic)
				{
					case int i:
						return i > 0 ? 1 : 0;

					case double d:
						return d > 0D ? 1 : 0;

					case float f:
						return f > 0F ? 1 : 0;

					case long l:
						return l > 0L ? 1 : 0;

					case bool flag:
						return flag ? 1 : 0;

					default:
						throw new ArgumentOutOfRangeException(logic.GetType().FullName);
				}
			}

			var text = GetEmptyStringIfNull(value, reader);

			if (text == null)
				return 0;

			if (text is string s)
			{
				switch (s.ToLower())
				{
					case "да":
					case "д":
					case "yes":
					case "y":
					case "х":
					case "x":
						return 1;

					case "нет":
					case "н":
					case "no":
					case "n":
					case "":
						return 0;

					default:
						throw new ArgumentOutOfRangeException(s);
				}
			}

			throw new InvalidOperationException(value.GetType().ToString());
		}
	}
}
