using System;
using IDD;
using IDD.iOS.ControlRender;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(EditorRoundedRenderer), typeof(EditorRoundedIos))]
namespace IDD.iOS.ControlRender
{
    public class EditorRoundedIos : EditorRenderer
    {
		string Placeholder { get; set; }

		protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
		{
			base.OnElementChanged(e);
			var element = Element as EditorRoundedRenderer;

			if (Control != null && element != null)
			{
				Placeholder = element.Placeholder;
				Control.TextColor = UIColor.LightGray;
				Control.Text = Placeholder;
				Control.Layer.CornerRadius = 10;
				Control.Layer.MasksToBounds = true;

				Control.ShouldBeginEditing += (UITextView textView) =>
				{
					if (textView.Text == Placeholder)
					{
						textView.Text = "";
						textView.TextColor = UIColor.Black; // Text Color
					}

					return true;
				};

				Control.ShouldEndEditing += (UITextView textView) =>
				{
					if (textView.Text == "")
					{
						textView.Text = Placeholder;
						textView.TextColor = UIColor.LightGray; // Placeholder Color
					}

					return true;
				};
			}
		}
    }
}
