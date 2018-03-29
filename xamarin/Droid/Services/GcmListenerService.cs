using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Gms.Gcm;
using Android.Util;

namespace IDD.Droid
{
	
[Service(Exported = false), IntentFilter(new[] { "com.google.android.c2dm.intent.RECEIVE" })]
public class MyGcmListenerService : GcmListenerService
{
		public override void OnMessageReceived(string from, Bundle data)
		{
				var message = data.GetString("message");
				var title = data.GetString("title");
				Console.WriteLine("MyGcmListenerService\", \"From:   " + from);
				Console.WriteLine("MyGcmListenerService\", \"Message:   " + message);

		
				SendNotification(message,title);
		}

		void SendNotification(string message,string title)
		{
			var intent = new Intent(this, typeof(MainActivity));
			intent.AddFlags(ActivityFlags.ClearTop);
			var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

			var notificationBuilder = new Notification.Builder(this)
			                                          .SetSmallIcon(Resource.Drawable.ic_login_clave)
				//.SetContentTitle("GCM Message")
			                                          .SetContentTitle(title)
				.SetContentText(message)
				.SetAutoCancel(true)
				.SetContentIntent(pendingIntent);

			var notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
			notificationManager.Notify(0, notificationBuilder.Build());
		}
		
	}
}
