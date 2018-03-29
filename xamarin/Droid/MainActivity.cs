using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.Permissions;

using Android.Gms.Common;
using Android.Util;


namespace IDD.Droid
{
   
	[Activity(Label = "IDD.Droid", Icon = "@drawable/icon_idd",Theme = "@style/MyTheme" , ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
		 	ToolbarResource = Resource.Layout.Toolbar;

            var r = this.ActionBar;
           // ActionBar.SetLogo(Resource.Drawable.home_header);

			base.OnCreate(bundle);

    //        if (IsPlayServicesAvailable())
		  //  {
		  //      var intent = new Intent(this, typeof(RegistrationIntentService));
				//StartService (intent);

		  //  }

			global::Xamarin.Forms.Forms.Init(this, bundle);
			PlotProjects.Plugin.Plot.GetInstance(this);

			Context context = ApplicationContext;
            App.CurrentVersion = context.PackageManager.GetPackageInfo(context.PackageName, 0).VersionName;
            Xamarin.FormsMaps.Init(this, bundle);

			LoadApplication(new App());


             
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
		{
			PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}


		public bool IsPlayServicesAvailable()
		{
			int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
			if (resultCode != ConnectionResult.Success)
			{
				if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))

					Console.WriteLine(GoogleApiAvailability.Instance.GetErrorString(resultCode));
				else
				{
					Console.WriteLine("Sorry, this device is not supported");
					Finish();
				}
				return false;
			}
			else
			{
				Console.WriteLine("Good google play services is available");
				return true;
		 }
		}


	}

}
