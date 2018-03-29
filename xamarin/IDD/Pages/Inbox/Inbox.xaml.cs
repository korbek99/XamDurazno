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
			 listInbox.ItemSelected += ListView_ItemSelected;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			Loading(true);
			LoadMain();
				
		}

		public void OnDelete(object sender, EventArgs e)
		{
			var  item = ((Xamarin.Forms.MenuItem)sender);

			if (!String.IsNullOrEmpty(item.CommandParameter.ToString())) {
				Loading(true);
				EliminarMensaje(item.CommandParameter.ToString());	
			}
			//DisplayAlert("Delete Context Action", item.CommandParameter + " delete context action", "OK");
		}



		public async void EliminarMensaje(string id)
		{
			    Console.WriteLine("entra a registrar");

		
				try
				{

					var responseApi = await InboxService.DeleteMessage(id);


					Device.BeginInvokeOnMainThread(async () =>
				   {
					   if (responseApi != null)
					   {

						   if (responseApi["codigo"].ToString() == "1")
						   {
							   var s = responseApi["data"].ToString();
							   LoadMain();
							  
						   }
						   else
						   {
							   Loading(false);
							   await DisplayAlert("Inbox", responseApi["mensaje"].ToString(), "Aceptar");

						   }

					   }
					   else
					   {
						   Loading(false);
						   await DisplayAlert("Inbox", "Ha ocurrido un error.", "Aceptar");
					   }

				   });

				}
				catch (Exception e)
				{
					Loading(false);
					Console.WriteLine("ha ocurrido un error");

				}

			}



				async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
				{
					var item = e.SelectedItem as Message;
					listInbox.SelectedItem = null;
					if (item != null)
					{
						await Navigation.PushAsync(new DetalleInboxPage(item));
					}
				}


				async Task LoadMain()
				{
					await Task.Run(async () =>
					{
						var data = await InboxService.GetMessages(App.userFB.id);

						Console.WriteLine("OK listas");
						Device.BeginInvokeOnMainThread(() =>
						{
							if (data != null){
								
								listInbox.IsVisible = true;
								listInbox.ItemsSource = data;
								stkInbox.IsVisible = false;

							}
							else { 
								
								stkInbox.IsVisible = true;
								listInbox.IsVisible = false;
							}
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
