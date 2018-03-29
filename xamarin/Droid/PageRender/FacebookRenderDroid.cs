using System;
using System.Json;
using System.Threading.Tasks;
using Android.App;
using IDD;
using IDD.Droid;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(FacebookPage), typeof(FacebookRenderDroid))]
namespace IDD.Droid
{
	public class FacebookRenderDroid : PageRenderer
	{

		public FacebookRenderDroid()
		{
			var activity = this.Context as Activity;

			var auth = new OAuth2Authenticator(
				clientId: "1656022791365456",
				scope: "public_profile,email,user_location,user_religion_politics",
				authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
				redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html"));
			App.HideLoginView();

			auth.Completed += async (sender, ee) =>
			{
				if (ee.IsAuthenticated)
				{
					var request = new OAuth2Request("GET", new Uri("https://graph.facebook.com/me?fields=political,location,hometown,email,birthday,first_name,last_name,gender,picture"), null, ee.Account);

					var response = await request.GetResponseAsync();

                    App.LoginFacebook(response.GetResponseText(), ee.Account.Properties["access_token"], DateTime.Now + TimeSpan.FromSeconds(Convert.ToDouble(ee.Account.Properties["expires_in"])));
					//App.NavigateToProfile();
				}
				else {
					App.NavigateToProfile();
				}
			};

			Context.StartActivity(auth.GetUI(Context));
		}

		static readonly TaskScheduler UIScheduler = TaskScheduler.FromCurrentSynchronizationContext();
	}
}
