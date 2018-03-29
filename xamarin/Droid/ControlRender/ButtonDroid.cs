using System;
using IDD.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Button), typeof(ButtonDroid))]
namespace IDD.Droid
{
	public class ButtonDroid : ButtonRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement == null)
			{
				Control.Background = null;
			}

		}
	}
}
