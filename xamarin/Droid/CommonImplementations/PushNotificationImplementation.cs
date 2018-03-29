
using Xamarin.Forms;
using IDD;
using IDD.Droid;
using Android.Content;
using System;
using Android.App;
using Android.Runtime;
using System.Threading.Tasks;

[assembly: Dependency(typeof(PushNotificationImplementation))]
namespace IDD.Droid
{


	public class PushNotificationImplementation : IPushNotification
	{
		
		public  void RegisterDevice(int userId,string email)
		{
				
				
				ISharedPreferences prefs = Android.App.Application.Context.GetSharedPreferences("PREF_NAME", FileCreationMode.Private);
				//TODO: testing borrar en deploy
				//ISharedPreferencesEditor editor = prefs.Edit();
				//editor.PutString("endpoint_arn", null);
				//editor.Apply();


				
				var endpoint_arn = prefs.GetString("endpoint_arn", null);
				

				if (String.IsNullOrEmpty(endpoint_arn))
				{
					var context = Android.App.Application.Context;
					var intent = new Intent(context, typeof(RegistrationIntentService));
					intent.PutExtra("email", email);
				    intent.PutExtra("user_id", userId);
					context.StartService(intent);
				}
				else { 
					Console.WriteLine("no entro a crear el endpoint" + endpoint_arn +"__");
				}

		}

	}


}
