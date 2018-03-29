using System;
using IDD;
using IDD.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ButtonFace), typeof(ButtonFacebookDroid))]
namespace IDD.Droid
{
	public class ButtonFacebookDroid : ButtonRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement == null)
			{
				Control.SetBackgroundResource(Resource.Drawable.btnFace);

			}

		}
	}
}
