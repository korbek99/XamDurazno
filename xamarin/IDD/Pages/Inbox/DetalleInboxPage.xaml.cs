using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace IDD
{
	public partial class DetalleInboxPage : ContentPage
	{
		Message message;
		public DetalleInboxPage()
		{
			InitializeComponent();
		}


		public DetalleInboxPage(Message _message)
		{
			InitializeComponent();
			message = _message;

			lblTitle.Text = message.title;
			lblMessage.Text = message.message;
			lblCreatedAt.Text = message.created_at;

		}



	}
}
