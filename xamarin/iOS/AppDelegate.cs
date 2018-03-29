using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Amazon;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
//using Facebook.CoreKit;
using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

//using Facebook.CoreKit;


namespace IDD.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : FormsApplicationDelegate
	{
		string appId = "1656022791365456";
		string appName = "intendencia durazno";

		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();

			//Profile.EnableUpdatesOnAccessTokenChange(true);
			//Settings.AppID = appId;
			//Settings.DisplayName = appName;
				

				//TODO:push notification falta integrar ios
				app.UnregisterForRemoteNotifications();
				var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(
				UIUserNotificationType.Alert |
				UIUserNotificationType.Badge |
				UIUserNotificationType.Sound,
				null
				);
				app.RegisterUserNotificationSettings(pushSettings);
				app.RegisterForRemoteNotifications();


			UISwitch.Appearance.OnTintColor =  Color.FromHex("#548EBD").ToUIColor();

			UINavigationBar.Appearance.TintColor = Color.White.ToUIColor();
			//establecer color de fondo sin esto, al levantar el album de photos no se distinguen los titulos
			UINavigationBar.Appearance.BarTintColor = Color.FromHex("#1C5885").ToUIColor();
			UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes()
			{
				TextColor = UIColor.White
			});
			//PlotProjects.Plugin.Plot.GetInstance(options, true);
			ImageCircleRenderer.Init();
            Xamarin.FormsMaps.Init();
            Xamarin.IQKeyboardManager.SharedManager.Enable = true;
            //IQKeyboardManager.SharedManager.Enable = true;

            App.CurrentVersion = NSBundle.MainBundle.InfoDictionary["CFBundleVersion"].ToString();			

			LoadApplication(new App());			

			return base.FinishedLaunching(app, options);
		}

		public override void RegisteredForRemoteNotifications(UIApplication application, NSData token)
		{

			Console.WriteLine("wtf=" + token.ToString() +"__");

			//TODO: registrar push deberia guardar el device token para ser llamado desde un codigo del forms al iniciar sesion
			//registrarPush(token);
			saveDeviceTokenForPush(token);

		}

		private void saveDeviceTokenForPush(NSData token) { 

			var deviceToken = token.Description.Replace("<", "").Replace(">", "").Replace(" ", "");

			NSUserDefaults.StandardUserDefaults.SetString(deviceToken,"device_token");
			NSUserDefaults.StandardUserDefaults.Synchronize();
		}


		private async void registrarPush(NSData token) { 
			
			var access_key = "AKIAJLTJVDVD3ECKFSBQ";
			var secret_key = "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";

			var snsClient = new AmazonSimpleNotificationServiceClient(access_key, secret_key, RegionEndpoint.USWest2);



			var deviceToken = token.Description.Replace("<", "").Replace(">", "").Replace(" ", "");

			Console.WriteLine("token=" + deviceToken +"__");


			if (!string.IsNullOrEmpty(deviceToken))
			{
				//register with SNS to create an endpoint ARN
				Console.WriteLine("token=" + deviceToken);
				var response = await snsClient.CreatePlatformEndpointAsync(
				new CreatePlatformEndpointRequest
				{
					CustomUserData = "jmancilla.zarate@gmail.com",
					Token = deviceToken,
					PlatformApplicationArn = "arn:aws:sns:us-west-2:490523474570:app/APNS_SANDBOX/idd_ios_dev" /* insert your platform application ARN here */
				});

				Console.WriteLine("arn_endoint ios" + response.EndpointArn+ "__");
		 	}
		}

		public override void ReceivedLocalNotification(UIApplication application, UILocalNotification notification)
		{
			Console.WriteLine(notification);

			//PlotProjects.Plugin.Plot.HandleNotification(notification);
		}

		public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
		{
			//base.ReceivedRemoteNotification(application, userInfo);

		//	var test = "Fadf";


		}
		//public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
		//{
		//	// We need to handle URLs by passing them to their own OpenUrl in order to make the SSO authentication works.
		//	return ApplicationDelegate.SharedInstance.OpenUrl(application, url, sourceApplication, annotation);
		//}

		public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
		{

			string data = url.Query;

			SaveResetPasswordData(data);

			return true;

		}

		private void SaveResetPasswordData(string data) { 
			
			if (!String.IsNullOrEmpty(data)) { 
			
				var parametros = data.Split('&');

				if (parametros.Length >= 3)
				{
					var userId = parametros[0].Split('=')[1];
					var token = parametros[1].Split('=')[1];
					var token_reset_at = parametros[2].Split('=')[1];


					NSUserDefaults.StandardUserDefaults.SetString(userId,"user_id");
					NSUserDefaults.StandardUserDefaults.SetString(token,"token");
					NSUserDefaults.StandardUserDefaults.SetString(token_reset_at,"token_reset_at");
					NSUserDefaults.StandardUserDefaults.Synchronize();

				}
			}
		}
	}
}
