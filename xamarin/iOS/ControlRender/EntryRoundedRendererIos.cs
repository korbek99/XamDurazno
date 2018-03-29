using System;
using System.ComponentModel;
using IDD;
using IDD.iOS.ControlRender;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(EntryRoundedRenderer), typeof(EntryRoundedRendererIos))]
namespace IDD.iOS.ControlRender
{
    public class EntryRoundedRendererIos : EntryRenderer
    {
		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

            Control.Layer.CornerRadius = 17;
            //Control.Layer.BorderWidth = 2;
            Control.Layer.MasksToBounds = true;
			Control.TextAlignment = UIKit.UITextAlignment.Natural;


			if (Control != null)
			{
				Control.SpellCheckingType = UITextSpellCheckingType.No;             // No Spellchecking
				Control.AutocorrectionType = UITextAutocorrectionType.No;           // No Autocorrection
				Control.AutocapitalizationType = UITextAutocapitalizationType.None; // No Autocapitalization
			}


			Control.ShouldReturn += (textField) =>
			{
				textField.ResignFirstResponder();
				return true;
			};
		}
    }
}
