using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserConverter
{
	public static class SourceCodeProvider
	{
		public const string Definition = "!@#$%^&*";

		public static string GetSource(string fileName)
		{
			using (StreamReader reader = new StreamReader(fileName))
				return reader.ReadToEnd();
		}

		public static void SetSource(string fileName, string value)
		{
			using (StreamWriter writer = new StreamWriter(fileName))
				writer.Write(value);
		}

		public static string GetEditorTemplate()
		{
			return GetSource(@"C:\Users\Jhayphal\source\repos\Jhayphal\UniversalDataConverter\UserConverter\Templates\PrepareValueCode.html");
		}

		public static string GetEditorAsHtmlContent(string sourceCode)
		{
			var template = GetEditorTemplate();

			return template.Replace(Definition, sourceCode);
		}
	}
}
