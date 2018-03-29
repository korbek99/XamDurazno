using System;

using Xamarin.Forms;

namespace IDD
{
    public class EditorRoundedRenderer : Editor
    {
		public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create("Placeholder", typeof(string), typeof(EditorRoundedRenderer), null);

		public string Placeholder
		{
			get
			{
				return (string)GetValue(PlaceholderProperty);
			}

			set
			{
				SetValue(PlaceholderProperty, value);
			}
		}
    }
}

