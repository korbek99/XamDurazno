using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IDD
{
    public partial class CreaContraseñaPage : ContentPage
    {
		ResetPasswordInfo info;

		TapGestureRecognizer tapTerminar;
		public CreaContraseñaPage()
        {
            InitializeComponent();
            Loading(false);
        }

		public CreaContraseñaPage(ResetPasswordInfo _info)
		{
			InitializeComponent();
			Loading(false);
			info = _info;

			tapTerminar = new TapGestureRecognizer();
			tapTerminar.Tapped += TapTerminar_Tapped;

			stkTerminar.GestureRecognizers.Add(tapTerminar);

		}


		async void TapTerminar_Tapped(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(txtPassword.Text) && !string.IsNullOrEmpty(txtRepeatPassword.Text))
			{
				if(txtPassword.Text == txtRepeatPassword.Text){

					if(txtPassword.Text.Length >= 4) {

                        Loading(true);
						await ResetPassword(txtPassword.Text);
					}else {
						await DisplayAlert("Uh oh!", "su contraseña debe tener minimo 4 caracteres", "Aceptar");
					}
                   
				}else {
					await DisplayAlert("Uh oh!", "Contraseñas no coinciden", "Aceptar");
				}

			}
			else
			{
				await DisplayAlert("Uh oh!", "Todos los datos son obligatorios", "Aceptar");
			}
				
		}

		public async Task ResetPassword(string password)
		{
			await Task.Run(async () =>
			{

				try
				{

					var responseApi = await UserService.PostResetPassword(info.userId,password,info.token);

					Device.BeginInvokeOnMainThread(async () =>
					{
						Loading(false);
						if (responseApi != null)
						{
							if (responseApi["codigo"].ToString() == "1")
							{
								App.GoToLogin();

							}
							else
							{
								await DisplayAlert("Recuperar Contraseña", responseApi["mensaje"].ToString(), "Aceptar");

							}

						}
						else
						{
							Loading(false);
							await DisplayAlert("Usuario", "Ha ocurrido un error", "Aceptar");
						}
					});

				}
				catch (Exception e)
				{
					Loading(false);
					await DisplayAlert("Usuario", "Ha ocurrido un error " + e.Message, "Aceptar");
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
