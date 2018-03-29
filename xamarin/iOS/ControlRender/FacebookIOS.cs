using System;
using System.Collections.Generic;
using System.Drawing;
using CoreGraphics;
//using Facebook.CoreKit;
//using Facebook.LoginKit;
using Foundation;
using IDD;
using IDD.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(FacebookForms), typeof(FacebookIOS))]
namespace IDD.iOS
{
    public class FacebookIOS : ViewRenderer<FacebookForms, UIView>
    {
        List<string> readPermissions = new List<string> { "public_profile", "email" };


		//LoginButton loginView;
		//ProfilePictureView pictureView;
		//UILabel nameLabel;
        public FacebookIOS()
        {
        }

		protected override void OnElementChanged(ElementChangedEventArgs<FacebookForms> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null)
			{

			}

			if (e.NewElement != null)
			{
    //           if (Facebook.CoreKit.AccessToken.CurrentAccessToken != null)
    //            {
                    
    //            }

				//Profile.Notifications.ObserveDidChange((sender, ex) =>
				//{

				//	if (ex.NewProfile == null)
				//		return;

    //                var r = Newtonsoft.Json.JsonConvert.SerializeObject(AccessToken.CurrentAccessToken);
    //                //AddUserInfoSection(ex.NewProfile.UserID);
				//	//nameLabel.Text = ex.NewProfile.Name;
				//});

				//// Set the Read and Publish permissions you want to get
				//loginView = new LoginButton(new CGRect(51, 0, 218, 46))
				//{
    //                LoginBehavior = LoginBehavior.SystemAccount,
				//	ReadPermissions = readPermissions.ToArray(),
				//};


				//// Handle actions once the user is logged in
				//loginView.Completed += (sender, ex) =>
				//{
				//	if (ex.Error != null)
				//	{
				//		// Handle if there was an error
				//	}

				//	if (ex.Result.IsCancelled)
				//	{
				//		// Handle if the user cancelled the login request
				//	}

				//	// Handle your successful login
				//};

				//// Handle actions once the user is logged out
				//loginView.LoggedOut += (sender, ex) =>
				//{
				//	// Handle your logout
				//};

				// The user image profile is set automatically once is logged in
				//pictureView = new ProfilePictureView(new CGRect(50, 50, 220, 220));

				// Create the label that will hold user's facebook name
				//nameLabel = new UILabel(new RectangleF(20, 319, 280, 21))
				//{
				//	TextAlignment = UITextAlignment.Center,
				//	BackgroundColor = UIColor.Clear
				//};

				// Add views to main view
				//View.AddSubview(loginView);
				//View.AddSubview(pictureView);
				//View.AddSubview(nameLabel);

				//var causas = (Element).causa;

				//table = new UITableView(new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height)); // defaults to Plain style
				//table.AutoresizingMask = UIViewAutoresizing.All;

				//tableDelegate = new CausaDelegate(causas, table, (Element).page, this);
				//var source = new InboxExpandableSource<Causa>(causas, table, tableDelegate, (Element).page);
				//table.Source = source;
				//table.AllowsSelection = false;

                //SetNativeControl(loginView);
			}
		}

		void AddUserInfoSection(string userId)
		{
			// Ask for the info that the user allowed to show
			//var fields = "?fields=id,name,email,birthday,about,hometown";
			//var request = new GraphRequest("/" + userId + fields, null, AccessToken.CurrentAccessToken.TokenString, null, "GET");
			//var requestConnection = new GraphRequestConnection();
			//requestConnection.AddRequest(request, (connection, result, error) =>
			//{
			//	// Handle if something went wrong with the request
			//	if (error != null)
			//	{
			//		//new UIAlertView("Error...", error.Description, null, "Ok", null).Show();
			//		return;
			//	}

			//	//NSDictionary userInfo = (result as NSDictionary);

			//	//var userSection = new Section(userInfo["name"].ToString());

			//	//// Show the info user that allowed to show
			//	//// If the user haven't assigned anything to his/her information,
			//	//// it will be null even if the user allowed to show it.
			//	//if (userInfo["email"] != null)
			//	//	userSection.Add(new StringElement("Email: " + userInfo["email"].ToString()));
			//	//if (userInfo["birthday"] != null)
			//	//	userSection.Add(new StringElement("Birthday: " + userInfo["birthday"].ToString()));
			//	//if (userInfo["about"] != null)
			//	//	userSection.Add(new StringElement("About: " + userInfo["about"].ToString()));
			//	//if (userInfo["hometown"] != null)
			//	//{
			//	//	var hometownData = userInfo["hometown"] as NSDictionary;
			//	//	userSection.Add(new StringElement("Hometown: " + hometownData["name"]));
			//	//}

			//	//Root.Add(userSection);

			//	//// Add the allowed actions that user have granted
			//	//AddActionsSection();
			//});
			//requestConnection.Start();
		}
    }
}
