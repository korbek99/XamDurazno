
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace IDD.Droid
{
	[Activity(Label = "Intendencia Durazno", NoHistory = true, Icon = "@drawable/icon_idd", Theme = "@style/Theme.Splash", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
			  ScreenOrientation = ScreenOrientation.Portrait)]
	public class SplashActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);


			// Create your application here
			Task startupWork = new Task(() =>
			{
				//SetContentView(Resource.Layout.Splash);
				Task.Delay(50000);  // Simulate a bit of startup work.
			});

			startupWork.ContinueWith(t =>
			{
				StartActivity(new Intent(Application.Context, typeof(MainActivity)));
			}, TaskScheduler.FromCurrentSynchronizationContext());

			startupWork.Start();
		}
	}
}
