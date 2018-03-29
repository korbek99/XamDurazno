using System;
using Android.App;
using IDD;
using IDD.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Content;
using Xamarin.Facebook;
using Android.Runtime;
using Xamarin.Facebook.Share.Widget;
using Xamarin.Facebook.Share;
using Xamarin.Facebook.Login;

[assembly: ExportRenderer(typeof(FacebookForms), typeof(FacebookDroid))]
namespace IDD.Droid
{
    public class FacebookDroid : ViewRenderer<View, Android.Views.View>
    {
		static readonly string[] PERMISSIONS = new[] { "public_profile", "email" };

		//const String PENDING_ACTION_BUNDLE_KEY = "com.facebook.samples.hellofacebook:PendingAction";
		//Button postStatusUpdateButton;
		//Button postPhotoButton;
		//ProfilePictureView profilePictureView;
		//TextView greeting;
		//PendingAction pendingAction = PendingAction.NONE;
		bool canPresentShareDialog;
		bool canPresentShareDialogWithPhotos;
		ICallbackManager callbackManager;
		ProfileTracker profileTracker;
		ShareDialog shareDialog;
		FacebookCallback<SharerResult> shareCallback;

        public FacebookDroid()
        {
        }

		protected override void OnElementChanged(ElementChangedEventArgs<View> e)
		{
            FacebookSdk.SdkInitialize(Android.App.Application.Context);

            callbackManager = CallbackManagerFactory.Create();

			var loginCallback = new FacebookCallback<LoginResult>
			{
				HandleSuccess = loginResult =>
				{
                    Console.WriteLine("1");
					//HandlePendingAction();
					//UpdateUI();
				},
				HandleCancel = () =>
				{
                    Console.WriteLine("2");
					//if (pendingAction != PendingAction.NONE)
					//{
					//	ShowAlert(
					//		GetString(Resource.String.cancelled),
					//		GetString(Resource.String.permission_not_granted));
					//	pendingAction = PendingAction.NONE;
					//}
					//UpdateUI();
				},
				HandleError = loginError =>
				{
                    Console.WriteLine("3");
					//if (pendingAction != PendingAction.NONE
					//	&& loginError is FacebookAuthorizationException)
					//{
					//	ShowAlert(
					//		GetString(Resource.String.cancelled),
					//		GetString(Resource.String.permission_not_granted));
					//	pendingAction = PendingAction.NONE;
					//}
					//UpdateUI();
				}
			};

            LoginManager.Instance.RegisterCallback(callbackManager, loginCallback);

			shareCallback = new FacebookCallback<SharerResult>
			{
				HandleSuccess = shareResult =>
				{
					Console.WriteLine("HelloFacebook: Success!");
				},
				HandleCancel = () =>
				{
					Console.WriteLine("HelloFacebook: Canceled");
				},
				HandleError = shareError =>
				{
					Console.WriteLine("HelloFacebook: Error: {0}", shareError);
				}
			};
			//profileTracker = new CustomProfileTracker
			//{
			//	HandleCurrentProfileChanged = (oldProfile, currentProfile) =>
			//	{
			//		UpdateUI();
			//		HandlePendingAction();
			//	}
			//};

			var r = (Android.Views.LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);
			var containerView = r.Inflate(Resource.Layout.FacebookButton, null, false);
			SetNativeControl(containerView);

			//Activity activity = Context as Activity;
			//var ft = activity.FragmentManager.BeginTransaction();
			//ft.Replace(Resource.Id.contentSwipe, new FragmentInbox(((SwipeCausaForms)Element).causa, activity, ((SwipeCausaForms)Element).page), "fragment_inbox");
			//ft.Commit();
		}
    }

	class FacebookCallback<TResult> : Java.Lang.Object, IFacebookCallback where TResult : Java.Lang.Object
	{
		public Action HandleCancel { get; set; }
		public Action<FacebookException> HandleError { get; set; }
		public Action<TResult> HandleSuccess { get; set; }

		public void OnCancel()
		{
            Console.WriteLine("4");
			var c = HandleCancel;
			if (c != null)
				c();
		}

		public void OnError(FacebookException error)
		{
            Console.WriteLine("5");
			var c = HandleError;
			if (c != null)
				c(error);
		}

		public void OnSuccess(Java.Lang.Object result)
		{
            Console.WriteLine("6");
			var c = HandleSuccess;
			if (c != null)
				c(result.JavaCast<TResult>());
		}
	}
}
