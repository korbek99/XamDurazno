using Xamarin.Forms;
using IDD;
using Foundation;
using IDD.iOS;

[assembly: Dependency(typeof(ResetPasswordImplementation))]
namespace IDD.iOS
{
	public class ResetPasswordImplementation:IResetPassword
	{
		public ResetPasswordInfo GetDataFromUriCall()
		{
			ResetPasswordInfo info = new ResetPasswordInfo();

			//var plist = NSUserDefaults.StandardUserDefaults;

			if (!string.IsNullOrEmpty(NSUserDefaults.StandardUserDefaults.StringForKey("user_id"))){ 
				info.userId = NSUserDefaults.StandardUserDefaults.StringForKey("user_id");
				info.token = NSUserDefaults.StandardUserDefaults.StringForKey("token");
				info.tokenResetAt = NSUserDefaults.StandardUserDefaults.StringForKey("token_reset_at");
			}
			return info;
		}

		public void CleanDataFromUriCall()
		{
			//var plist = NSUserDefaults.StandardUserDefaults;
			NSUserDefaults.StandardUserDefaults.SetString(string.Empty,"user_id");
			NSUserDefaults.StandardUserDefaults.SetString(string.Empty,"token");
			NSUserDefaults.StandardUserDefaults.SetString(string.Empty,"token_reset_at");

			NSUserDefaults.StandardUserDefaults.Synchronize();
		}
	}
}
