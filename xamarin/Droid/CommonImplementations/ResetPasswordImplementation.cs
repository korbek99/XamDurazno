
using Xamarin.Forms;
using IDD.Droid;
using IDD;
using Android.Content;
using System;

[assembly: Dependency(typeof(ResetPasswordImplementation))]
namespace IDD.Droid
{
	public class ResetPasswordImplementation:IResetPassword
	{


		public ResetPasswordInfo GetDataFromUriCall()
		{
			ISharedPreferences prefs = Android.App.Application.Context.GetSharedPreferences("PREF_NAME", FileCreationMode.Private);

			ResetPasswordInfo info = new ResetPasswordInfo();
		
			if (string.IsNullOrEmpty(info.userId)) {
				
				info.userId = prefs.GetString("user_id", string.Empty);
				info.token = prefs.GetString("token", string.Empty);
				info.tokenResetAt = prefs.GetString("token_reset_at", string.Empty);
			}

			return info;

		}

		public void CleanDataFromUriCall()
		{
			ISharedPreferences prefs = Android.App.Application.Context.GetSharedPreferences("PREF_NAME", FileCreationMode.Private);
			ISharedPreferencesEditor editor = prefs.Edit();

			editor.PutString("user_id",null );
			editor.PutString("token", null);
			editor.PutString("token_reset_at",null);

			editor.Apply();


		}
	}
}
