using System;
using IDD;
using IDD.iOS;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(FacebookPage), typeof(FacebookRenderIOS))]
namespace IDD.iOS
{
    public class FacebookRenderIOS : PageRenderer
    {
        OAuth2Authenticator auth;
        UIKit.UIViewController ui_object;
        public FacebookRenderIOS()
        {
			//var activity = this.Context as Activity;

			
            //PresentViewController(auth.GetUI(), true, null);
			//Context.StartActivity(auth.GetUI(Context));
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

			var authPage = Element as FacebookPage;

			auth = new OAuth2Authenticator(
				clientId: "1656022791365456",
				scope: "",
				authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
				redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html"));
			//App.HideLoginView();

			auth.Completed += async (sender, ee) =>
			{
                DismissViewController(true, null);
                authPage.Navigation.PopAsync();
                //App.Navigator.PopAsync();
				if (ee.IsAuthenticated)
				{
                    //AccountStore.Create().Save(ee.Account, "Facebook");

					var request = new OAuth2Request("GET", new Uri("https://graph.facebook.com/me?fields=political,location,hometown,email,birthday,first_name,last_name,gender,picture"), null, ee.Account);

					var response = await request.GetResponseAsync();

                    App.LoginFacebook(response.GetResponseText(), ee.Account.Properties["access_token"], DateTime.Now + TimeSpan.FromSeconds(Convert.ToDouble(ee.Account.Properties["expires_in"])));
					//App.NavigateToProfile();
				}
				else
				{
					App.NavigateToProfile();
				}
			};
			//ui_object = auth.GetUI();
            //ui_object.DismissViewController(true, null);
			PresentViewController(auth.GetUI(), true, null);
        }
    }
}
