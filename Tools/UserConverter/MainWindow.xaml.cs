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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UserConverter
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private async void webView_Loaded(object sender, RoutedEventArgs e)
		{
			await codeView.EnsureCoreWebView2Async();

			var source = SourceCodeProvider.GetSource(@"C:\Users\Jhayphal\source\repos\Jhayphal\UniversalDataConverter\UserConverter\Test.cs");
			
			var html = SourceCodeProvider.GetEditorAsHtmlContent(source);

			//codeView.NavigateToString(html);

			codeView.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;
		}

		private void CoreWebView2_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
		{
			string code = e.TryGetWebMessageAsString();

			SourceCodeProvider.SetSource(@"C:\Users\Jhayphal\source\repos\Jhayphal\UniversalDataConverter\UserConverter\Test.cs", code);
		}

		private void Save_Click(object sender, RoutedEventArgs e)
		{
			codeView.CoreWebView2.ExecuteScriptAsync("window.chrome.webview.postMessage(editor.getValue());");
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			var wnd = new CodeEditor(@"C:\Users\Jhayphal\source\repos\Jhayphal\UniversalDataConverter\UserConverter\Test.cs");

			wnd.Show();
		}
	}

	
}
