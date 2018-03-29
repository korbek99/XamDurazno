using System;
using Xamarin.Forms;
using IDD;
using IDD.IOS;
using System.Threading.Tasks;
using Amazon.SimpleNotificationService;
using Amazon;
using Amazon.SimpleNotificationService.Model;
using Foundation;

[assembly: Dependency(typeof(PushNotificationImplementation))]
namespace IDD.IOS
{
	public class PushNotificationImplementation : IPushNotification
	{

		public void RegisterDevice(int userId,string email)
		{
			Console.WriteLine("registrar device on ios");
			//return "ok pronto vas a tener un token";

			//var intent = new Intent(this, typeof(RegistrationIntentService));
			//StartService (intent);


			var deviceToken = NSUserDefaults.StandardUserDefaults.StringForKey("device_token");

			if (!String.IsNullOrEmpty(deviceToken))
			{
				SendRegistrationToAppServer(userId, email,deviceToken);
			}


		}

		async void SendRegistrationToAppServer(int userId, string email,string deviceToken)
		{
			var access_key = "AKIAJLTJVDVD3ECKFSBQ";
			var secret_key = "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";

			var snsClient = new AmazonSimpleNotificationServiceClient(access_key, secret_key, RegionEndpoint.USWest2);

			//TODO cambiar en paso a produccion
			var applicationArn = "arn:aws:sns:us-west-2:490523474570:app/APNS_SANDBOX/idd_ios_dev";

			Console.WriteLine("token=" + deviceToken +"__");


			if (!string.IsNullOrEmpty(deviceToken))
			{
				//register with SNS to create an endpoint ARN
				Console.WriteLine("token=" + deviceToken);
				var response = await snsClient.CreatePlatformEndpointAsync(
				new CreatePlatformEndpointRequest
				{
					CustomUserData = email,
					Token = deviceToken,
					PlatformApplicationArn = applicationArn/* insert your platform application ARN here */
				});

				Console.WriteLine("arn_endoint ios" + response.EndpointArn+ "__");

				var responseApi = await IDD.UserService.PostRegisterSNSDevice(userId, response.EndpointArn, applicationArn);

				if (responseApi != null)
				{
					if (responseApi["codigo"].ToString() == "1")
					{
						var s = responseApi["data"].ToString();
						Console.WriteLine("se guardo correctmente el token" + userId.ToString()  + "_" + response.EndpointArn);
					   //ISharedPreferences prefs = Application.Context.GetSharedPreferences("PREF_NAME", FileCreationMode.Private);
				       //ISharedPreferencesEditor editor = prefs.Edit();
				       //editor.PutString("endpoint_arn", response.EndpointArn);
						//editor.Apply();
					}
				}
		 	}
		}

	}
}
