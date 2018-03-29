
using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;


namespace IDD.Droid
{
[	Activity( NoHistory = true, Icon = "@drawable/icon_idd", Theme = "@style/Theme.Splash", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
			  ScreenOrientation = ScreenOrientation.Portrait)]
	[IntentFilter(new[]
	{ Intent.ActionView },
				  Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
				  DataScheme = "com.ewin.intendenciadurazno")]
	public class urlentryclass : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your application here
			Intent outsideIntent = Intent;

			SaveResetPasswordData(Intent.Data);

			StartActivity(typeof(MainActivity));

		}

		private void SaveResetPasswordData(Android.Net.Uri data) { 

			var userId = data.GetQueryParameter("user_id");
			var token = data.GetQueryParameter("token");
			var token_reset_at = Intent.Data.GetQueryParameter("token_reset_at");

			if (!String.IsNullOrEmpty(userId) &&
			   !String.IsNullOrEmpty(token) &&
			    !String.IsNullOrEmpty(token_reset_at)){ 

				//save token,token_reset_at,userid
				ISharedPreferences prefs = Application.Context.GetSharedPreferences("PREF_NAME", FileCreationMode.Private);

				ISharedPreferencesEditor editor = prefs.Edit();

				editor.PutString("user_id",userId );
				editor.PutString("token", token);
				editor.PutString("token_reset_at",token_reset_at);

				editor.Apply();

			}


		}


	}
}
