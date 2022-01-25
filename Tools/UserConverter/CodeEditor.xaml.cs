using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UserConverter
{
	/// <summary>
	/// Interaction logic for CodeEditor.xaml
	/// </summary>
	public partial class CodeEditor : Window
	{
		public readonly string CodeFileName;

		public CodeEditor(string codeFileName)
		{
			if (string.IsNullOrWhiteSpace(codeFileName))
				throw new ArgumentException(nameof(codeFileName));

			if (!File.Exists(codeFileName))
				throw new FileNotFoundException(codeFileName);

			CodeFileName = codeFileName;

			InitializeComponent();
		}

		private async void webView_Loaded(object sender, RoutedEventArgs e)
		{
			await codeView.EnsureCoreWebView2Async();

			var source = SourceCodeProvider.GetSource(CodeFileName);
			var html = SourceCodeProvider.GetEditorAsHtmlContent(source);

			codeView.NavigateToString(html);

			codeView.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;
		}

		private void CoreWebView2_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
		{
			string code = e.TryGetWebMessageAsString();

			SourceCodeProvider.SetSource(CodeFileName, code);
		}
	}
}
