using System;
using System.ComponentModel;
using IDD;
using IDD.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(EditorRoundedRenderer), typeof(EditorRendererDroid))]
namespace IDD.Droid
{
    public class EditorRendererDroid : EditorRenderer
    {
		protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null)
			{
				var element = e.NewElement as EditorRoundedRenderer;
				Control.Hint = element.Placeholder;
				Control.TextSize = 15;
				Control.SetBackgroundResource(Resource.Drawable.rounded_edittext);
				Control.SetPadding(25, 10, 0, 0);
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == EditorRoundedRenderer.PlaceholderProperty.PropertyName)
			{
				var element = Element as EditorRoundedRenderer;
				Control.Hint = element.Placeholder;
			}
		}
    }
}
