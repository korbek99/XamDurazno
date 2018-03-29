using System;
using IDD;
using IDD.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ButtonFace), typeof(ButtonFacebookIOS))]
namespace IDD.iOS
{
	public class ButtonFacebookIOS : ButtonRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				// do whatever you want to the UITextField here!
				UIButton btn = Control as UIButton;

				btn.Layer.BorderWidth = 1;
				btn.Layer.CornerRadius = e.OldElement==null ? e.NewElement.BorderRadius : e.OldElement.BorderRadius;
				btn.Layer.BorderColor = UIColor.White.CGColor;
			}
		}
	}
}
