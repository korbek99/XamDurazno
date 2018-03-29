using System;
using IDD;
using IDD.Droid.ControlRender;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(EntryRoundedRenderer), typeof(EntryRoundedRendererDroid))]
namespace IDD.Droid.ControlRender
{
	public class EntryRoundedRendererDroid : EntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement.BackgroundColor != Color.Transparent)
            {
                e.NewElement.BackgroundColor = Color.Transparent;
                //e.NewElement.BackgroundColor = Color.White;
                Control.TextSize = 15;
                Control.SetBackgroundResource(Resource.Drawable.rounded_edittext);
                Control.SetPadding(25, 10, 0, 0);
            }
        }
	}
}
