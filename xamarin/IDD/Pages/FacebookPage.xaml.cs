using System;
using System.Collections.Generic;
using System.Json;
using Xamarin.Forms;

//using System;
//using System.Collections.Generic;
using System.Text;
using System.Linq;

//using Xamarin.Forms;

using Xamarin.Auth;
//using Xamarin.Auth.XamarinForms;

//using Xamarin.Auth.SampleData;

namespace IDD
{
	public partial class FacebookPage : ContentPage
	{
		/// <summary>
		/// Make sure to get a new ClientId from:
		/// https://developers.facebook.com/apps/
		/// </summary>
		private string ClientId = "1656022791365456";
		double expire_in = 0;

		public FacebookPage()
		{
			InitializeComponent();

			//var auth = new OAuth2Authenticator(
			//	clientId: "1656022791365456",
			//	scope: "",
			//	authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
   //             redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html"), isUsingNativeUI: true);


			//auth.Completed += (sender, eventArgs) =>
			//{
			//	// UI presented, so it's up to us to dimiss it on Android
			//	// dismiss Activity with WebView or CustomTabs
			//	//this.Finish();
   //             //App.HideLoginView();
			//	if (eventArgs.IsAuthenticated)
			//	{
			//		// Use eventArgs.Account to do wonderful things
			//	}
			//	else
			//	{
			//		// The user cancelled
			//	}
			//};

    //        try
    //        {
				//var apiRequest =
				//"https://www.facebook.com/dialog/oauth?client_id="
				//+ ClientId
				//+ "&display=popup&response_type=token&redirect_uri=http://www.facebook.com/connect/login_success.html";

				//var webView = new WebView
				//{
				//	Source = apiRequest,
				//	HeightRequest = 1
				//};

				//webView.Navigated += WebViewOnNavigated;

				//Content = webView;
            //}
            //catch(Exception e)
            //{
                
            //}
			
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

			//Xamarin.Auth.Helpers.OAuth2 oauth_facebook = null;
			//oauth_facebook = Data.TestCases[provider] as Xamarin.Auth.Helpers.OAuth2;
			//AuthenticateOAuth2(oauth_facebook);
        }
	}
}
