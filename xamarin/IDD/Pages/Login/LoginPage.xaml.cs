using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IDD
{
	public partial class LoginPage : ContentPage
	{
        TapGestureRecognizer tapIniciarComoAnonimo;
        TapGestureRecognizer tapOlvido;
        TapGestureRecognizer tapFb;
		TapGestureRecognizer tapIniciarSesion;

		public LoginPage()
		{
			InitializeComponent();

			NavigationPage.SetHasNavigationBar(this, false);
           

			tapIniciarComoAnonimo = new TapGestureRecognizer();
            tapIniciarComoAnonimo.Tapped += (sender, e) => { App.NavigateToProfile(); };


			tapIniciarSesion = new TapGestureRecognizer();
			tapIniciarSesion.Tapped += TapIniciarSesion_Tapped;

			slIniciarSesion.GestureRecognizers.Add(tapIniciarSesion);

           // lblCrearCuenta.GestureRecognizers.Add(tapCrearCuenta);


			lblIngresarAnonimo.GestureRecognizers.Add(tapIniciarComoAnonimo);



            tapOlvido = new TapGestureRecognizer();
            tapOlvido.Tapped += async(sender, e) => { 
                await Navigation.PushAsync(new RecuperarContraseñaPage()); 
            };

            lblOlvidar.GestureRecognizers.Add(tapOlvido);

            tapFb = new TapGestureRecognizer();
            tapFb.Tapped += async (object sender, EventArgs e) => { 
                await Navigation.PushAsync(new FacebookPage()); 
            };

            imgFb.GestureRecognizers.Add(tapFb);

			Loading(false);
		}


		async void TapIniciarSesion_Tapped(object sender, EventArgs e)
		{


			if (!string.IsNullOrEmpty(txtMail.Text) && !string.IsNullOrEmpty(txtPassword.Text))
			{

				Loading(true);
				await IniciarSesion(txtMail.Text, txtPassword.Text);
			}
			else { 
				  await DisplayAlert("Uh oh!","Todos los datos son obligatorios", "Aceptar");
			}


		}

		public  async Task IniciarSesion(string email,string password)
		{
			Console.WriteLine("entra a registrar");

			await Task.Run(async () =>
			{

				try
				{

					var responseApi = await UserService.PostLogin(email,password);


					Device.BeginInvokeOnMainThread(async () =>
				   {
					   if (responseApi != null)
					   {

						   if (responseApi["codigo"].ToString() == "1")
						   {
							   var s = responseApi["data"].ToString();
							   var userLogged = Newtonsoft.Json.JsonConvert.DeserializeObject<UserFB>(s);

							   if (userLogged != null)
							   {
								   GeneralSetting.UserSettings = JsonConvert.SerializeObject(userLogged);
								   App.NavigateToProfile();
								   Loading(false);

							   }
						   }
						   else
						   {
							   Loading(false);
							   await DisplayAlert("Usuario", responseApi["mensaje"].ToString(), "Aceptar");

						   }

					   }
					   else {
						   Loading(false);
							await DisplayAlert("Usuario","Ha ocurrido un error.", "Aceptar");
						}

				   });

				}
				catch (Exception e)
				{
					Loading(false);
					Console.WriteLine("ha ocurrido un error");

				}

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
