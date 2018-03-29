using System;
using Android.App;
using Android.Content;
using Android.Util;
using Android.Gms.Gcm;
using Android.Gms.Iid;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Amazon;
using Amazon.CognitoIdentity;


namespace IDD.Droid
{



[Service(Exported = true)]
class RegistrationIntentService : IntentService
{
	static object locker = new object();

	public RegistrationIntentService() : base("RegistrationIntentService") { }


		protected override void OnHandleIntent(Intent intent)
		{
			try
			{


				Console.WriteLine("registration on handle token...");

				lock (locker)
				{
					var senderId = "608721161620"; //project name in google cloud message
					var instanceID = InstanceID.GetInstance(this);
					var token = instanceID.GetToken(
					senderId, GoogleCloudMessaging.InstanceIdScope, null);
					Console.WriteLine("registration token" + token);


					var email = intent.GetStringExtra("email");
					var userId = intent.GetIntExtra("user_id",0);


					Console.WriteLine("userid__"+userId+"_email:" + email);



					Console.WriteLine("email:::" + email);


					if (!String.IsNullOrEmpty(email)){ 
						SendRegistrationToAppServer(userId, email,token);
						Subscribe(token);
					}


	 			
				}
			}
			catch (Exception e)
			{
				Log.Debug("RegistrationIntentService", "Failed to get a registration token");
				return;
			}
	}



	async void SendRegistrationToAppServer(int userId,string email,string token)
	{

			var access_key = "AKIAJLTJVDVD3ECKFSBQ";
			var secret_key = "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";
			var applicationArn = "arn:aws:sns:us-west-2:490523474570:app/GCM/idd_android";

			var snsClient = new AmazonSimpleNotificationServiceClient(access_key,secret_key,RegionEndpoint.USWest2);

			Console.WriteLine(snsClient.ToString());

			var response = await snsClient.CreatePlatformEndpointAsync(new CreatePlatformEndpointRequest
			{
				CustomUserData = email,
				Token = token,
				PlatformApplicationArn = applicationArn

			});



			var responseApi = await IDD.UserService.PostRegisterSNSDevice(userId,response.EndpointArn,applicationArn);

			if (responseApi != null)
			{

				if (responseApi["codigo"].ToString() == "1")
				{
					var s = responseApi["data"].ToString();
					Console.WriteLine("se guardo correctmente el token" + userId.ToString()  + "_" + response.EndpointArn);

					ISharedPreferences prefs = Application.Context.GetSharedPreferences("PREF_NAME", FileCreationMode.Private);
					ISharedPreferencesEditor editor = prefs.Edit();
					editor.PutString("endpoint_arn", response.EndpointArn);
					editor.Apply();
				}
			}

			Console.WriteLine("endpoint arn___"+ response.EndpointArn+"___");

	}

	void Subscribe(string token)
	{
		var pubSub = GcmPubSub.GetInstance(this);
		pubSub.Subscribe(token, "/topics/idd_reclamo_estado", null);
	}

	}

}
