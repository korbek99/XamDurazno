using System;
using System.Collections.Generic;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace IDD
{
	public partial class MenuPage : ContentPage
	{
		public CircleImage ImgPerfil { get { return imgPerfil; } }
		public Label LblName { get { return lblName; } }
	
		public TapGestureRecognizer tapRegistrar { get; }

		public ListView ListView { get { return listView; } }

		bool isLogged = false;
		public MenuPage()
		{
			InitializeComponent();

			tapRegistrar = new TapGestureRecognizer();
			tapRegistrar.Tapped += TapRegistrar_Tapped;
			btnRegistrar.GestureRecognizers.Add(tapRegistrar);



            imgPerfil.Source = "sin_foto";
		
			lblVersion.Text = "Versión "+App.CurrentVersion;
            btnRegistrar.IsVisible = false;
			btnLogOut.IsVisible = false;


			CargarMenu();
		}


		void LogOut_Clicked(object sender, System.EventArgs e)
		{
			//await DisplayAlert("Alerta!", "vas a cerrar sesion", "Aceptar");
			GeneralSetting.UserSettings = null;
			App.GoToLogin();
		}


		//async void TapLogOut_Tapped(object sender, EventArgs e)
		//{

		//}

		private void CargarMenu() { 
			
			//limpiar items
			listView.ItemsSource = null;

			var masterPageItems = new List<Item>();

			masterPageItems.Add(new Item
			{
				Title = "Home",
				IconSource = "menu_home.png",
				TargetType = typeof(MainPage)
			});

			if (isLogged) { 
				masterPageItems.Add(new Item
				{
					Title = "Mis Reclamos",
					IconSource = "ic_menu_reclamo.png",
					TargetType = typeof(MisReclamosPage)
				});
			}


			if (isLogged) { 
				masterPageItems.Add(new Item
				{
					Title = "Inbox",
					IconSource = "ic_login_mail.png",
					TargetType = typeof(InboxPage)
				});
			}



			masterPageItems.Add(new Item
			{
				Title = "Campañas",
				IconSource = "ic_menu_campana.png",
				TargetType = typeof(CampanasPage)
			});

			masterPageItems.Add(new Item
			{
				Title = "Calendario de Pago",
				IconSource = "menu_calendario.png",
				TargetType = typeof(CalendarioPagoPage)
			});
			masterPageItems.Add(new Item
			{
				Title = "Lugares que visitar",
				IconSource = "menu_tour.png",
				TargetType = typeof(TourPage)
			});
			masterPageItems.Add(new Item
			{
				Title = "Noticias",
				IconSource = "menu_noticias.png",
				TargetType = typeof(NoticiasPage)
			});
			masterPageItems.Add(new Item
			{
				Title = "Otros",
				IconSource = "ic_menu_otros.png",
				TargetType = typeof(OtrosPage)
			});

			listView.ItemsSource = masterPageItems;

		}


		async void TapRegistrar_Tapped(object sender, EventArgs e)
		{
			App.GoToRegistrar();
		}

        public void IsLoggin(bool _isLogged)
        {
			isLogged = _isLogged;
			//usuario logeado
			if (isLogged)
			{
				btnRegistrar.IsVisible = false;
				btnLogOut.IsVisible = true;

				LblName.Text = "Bienvenido " + App.userFB.full_name;

				if (String.IsNullOrEmpty(App.userFB.url)){
					imgPerfil.Source = "sin_foto";
				}
				else { 
					ImgPerfil.Source = App.userFB.url;
				}
				RegistrarPushToken();

			}
			else
			{
             
                //LblName.Text = "¡Bienvenido";
                imgPerfil.Source = "sin_foto";
				btnRegistrar.IsVisible = true;
				btnLogOut.IsVisible = false;
			}

			CargarMenu();
        }

		public void RegistrarPushToken()
		{
			Console.WriteLine("usuario logeado");

			var data = App.userFB.email;
			if (String.IsNullOrEmpty(App.userFB.email)){
				data = App.userFB.phone;
			}
			DependencyService.Get<IPushNotification>().RegisterDevice(App.userFB.id,data);

		}

	
	}
}
