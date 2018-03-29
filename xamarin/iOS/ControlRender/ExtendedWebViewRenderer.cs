using System;
using IDD.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using IDD;
using UIKit;

[assembly: ExportRenderer(typeof(ExtendedWebView), typeof(ExtendedWebViewRenderer))]
namespace IDD.iOS
{
	
	public class ExtendedWebViewRenderer: WebViewRenderer
	{
		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);
			Delegate = new ExtendedUIWebViewDelegate(this); 
		}
	}

	public class ExtendedUIWebViewDelegate : UIWebViewDelegate
	{
		ExtendedWebViewRenderer webViewRenderer;

		public ExtendedUIWebViewDelegate(ExtendedWebViewRenderer _webViewRenderer = null)
		{
			webViewRenderer = _webViewRenderer ?? new ExtendedWebViewRenderer();
		}

		public override async void LoadingFinished(UIWebView webView)
		{
			var wv = webViewRenderer.Element as ExtendedWebView;
			if (wv != null)
			{
				await System.Threading.Tasks.Task.Delay(100); // wait here till content is rendered
				Console.WriteLine("webview height __" + webView.ScrollView.ContentSize.Height.ToString());
				double h = 200;
					wv.HeightRequest = (double)webView.ScrollView.ContentSize.Height;
			}
		}
	}
}
