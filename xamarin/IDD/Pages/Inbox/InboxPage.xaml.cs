using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IDD
{
	public partial class InboxPage : ContentPage
	{
		public InboxPage()
		{
			 InitializeComponent();
             Loading(false);
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			Loading(true);
			LoadMain();
				
		}

		async Task LoadMain()
		{
			await Task.Run(async () =>
			{
				var data = await InboxService.GetMessages(App.userFB.id);

				Console.WriteLine("OK listas");
				Device.BeginInvokeOnMainThread(() =>
				{
					if (data != null)
						listInbox.ItemsSource = data;
					Loading(false);
				});

			});
		}


		void Loading(bool isVisible)
		{
			boxIndicator.IsVisible = isVisible;
			actIndicator.IsRunning = isVisible;
			actIndicator.IsVisible = isVisible;
		}
	}
}
