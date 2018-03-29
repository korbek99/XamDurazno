using System;

using Xamarin.Forms;

namespace IDD
{
	public class DetalleInbox : ContentPage
	{
		public DetalleInbox()
		{
			Content = new StackLayout
			{
				Children = {
					new Label { Text = "Hello ContentPage" }
				}
			};
		}
	}
}

