using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;


namespace IDD
{
    public partial class RecuperarContraseñaPage : ContentPage
    {

		TapGestureRecognizer tapSiguiente;
        public RecuperarContraseñaPage()
        {
            InitializeComponent();

			tapSiguiente = new TapGestureRecognizer();
			tapSiguiente.Tapped += TapSiguiente_Tapped;
            slSiguiente.GestureRecognizers.Add(tapSiguiente);
            Loading(false);
        }


	async void TapSiguiente_Tapped(object sender, EventArgs e)
	{
		//validar datos primero
		var pasa_validacion = true;

		if (!string.IsNullOrEmpty(txtMail.Text))
		{
			//si viene el mail entonces validar 
			if (!string.IsNullOrEmpty((txtMail.Text)))
			{
				if (!GlobalHelpers.EmailBienEscrito(txtMail.Text))
				{
					await DisplayAlert("¡Uh oh!", "Email no valido.", "Aceptar");
					pasa_validacion = false;
				}
			}
			if (pasa_validacion)
			{
					Loading(true);
					await SolicitarCambioContraseña(txtMail.Text);
			}

		}
		else
		{

			await DisplayAlert("¡Uh oh!", "Todos los datos son obligatorios.", "Aceptar");
		}

	}
	

	public async Task SolicitarCambioContraseña(string email)
	{
		await Task.Run(async () =>
		{

			try
			{

				var responseApi = await UserService.PostResetPasswordRequest(email);

				Device.BeginInvokeOnMainThread(async () =>
				{
                    Loading(false);
					if (responseApi != null)
					{
						if (responseApi["codigo"].ToString() == "1")
						{
								await DisplayAlert("Recuperar Contraseña", "te hemos enviado un link al mail registrado. Debes seguir las instrucciones allí indicadas para continuar con el proceso.", "Aceptar");
								App.NavigateToProfile();
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
