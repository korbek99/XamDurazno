using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Xamarin.Forms;

namespace IDD
{
    public partial class RegistroPage : ContentPage
    {
        TapGestureRecognizer tapSiguiente;
		TapGestureRecognizer tapFb;
		UserFB userInfoFromFaceBook;

		public RegistroPage()
        {
            InitializeComponent();

            tapSiguiente = new TapGestureRecognizer();
            tapSiguiente.Tapped += TapSiguiente_Tapped;
            slSiguiente.GestureRecognizers.Add(tapSiguiente);
			var r = new ToolbarItem();
			r.Text = "Saltar";
			r.Clicked += (object sender, EventArgs e) => {
                App.BackToMain(false);
			};
			ToolbarItems.Add(r);


			tapFb = new TapGestureRecognizer();
			tapFb.Tapped += async (object sender, EventArgs e) =>
			{
				var aceptoTerminos = swicthTerminosYCondiciones.IsToggled;

				if (aceptoTerminos)
				{
					await Navigation.PushAsync(new FacebookPage());
				}
				else
				{
					await DisplayAlert("¡Uh oh!", "no se puede registrar si no acepta los terminos y condiciones.", "Aceptar");
				}
			};


			imgFb.GestureRecognizers.Add(tapFb);

			Loading(false);
        }


		public RegistroPage(UserFB user)
		{
			InitializeComponent();

			tapSiguiente = new TapGestureRecognizer();
			tapSiguiente.Tapped += TapSiguiente_Tapped;
			slSiguiente.GestureRecognizers.Add(tapSiguiente);
			var r = new ToolbarItem();
			r.Text = "Ir al Home";
			r.Clicked += (object sender, EventArgs e) =>
			{
				App.BackToMain(false);
			};
			ToolbarItems.Add(r);


			userInfoFromFaceBook = user;

			if (userInfoFromFaceBook != null) {
				txtMail.Text = userInfoFromFaceBook.email;
				txtPhone.Text = userInfoFromFaceBook.phone;
				txtFullName.Text = userInfoFromFaceBook.full_name;
				//txtMail.IsEnabled = false; porque algunas cuentas de facebook vienen sin email ejemplo las creadas con numero de telefono
				lblSiguiente.Text = "Terminar";
				swicthTerminosYCondiciones.IsToggled = true;

			}

			imgFb.IsVisible = false;
			btnSignIn.IsVisible = false;

			Loading(false);


		}


        async void TapSiguiente_Tapped(object sender, EventArgs e)
        {
            //validar datos primero
            var pasa_validacion = true;

			var aceptoTerminos = swicthTerminosYCondiciones.IsToggled;

			if (aceptoTerminos)
			{



				if (!string.IsNullOrEmpty(txtFullName.Text) && !string.IsNullOrEmpty(txtMail.Text) && !string.IsNullOrEmpty(txtPhone.Text))
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
						//los datos ya vienen del login de facebook
						if (userInfoFromFaceBook != null)
						{

							userInfoFromFaceBook.phone = txtPhone.Text;
							userInfoFromFaceBook.email = txtMail.Text;
							userInfoFromFaceBook.full_name = txtFullName.Text;


							Loading(true);
							await Registrar(userInfoFromFaceBook);
							//GeneralSetting.UserSettings = JsonConvert.SerializeObject(userInfoFromFaceBook);
							//App.NavigateToProfile();

						}
						else
						{

							UserFB user = new UserFB();
							user.email = txtMail.Text;
							user.full_name = txtFullName.Text;
							user.phone = txtPhone.Text;

							await Navigation.PushAsync(new ContraseñaPage(user));
						}

					}


				}
				else
				{

					await DisplayAlert("¡Uh oh!", "Todos los datos son obligatorios.", "Aceptar");
				}

			}
			else { 
					await DisplayAlert("¡Uh oh!", "no se puede registrar si no acepta los terminos y condiciones.", "Aceptar");
			}
        }


		public async Task Registrar(UserFB usr)
		{
			Console.WriteLine("entra a registrar");

			await Task.Run(async () =>
			{

				try
				{

					var responseApi = await FbService.PostUserFB(usr);

					Device.BeginInvokeOnMainThread(async () =>
					{

						if (responseApi != null)
						{
							if (responseApi["codigo"].ToString() == "1")
							{
								var s = responseApi["data"].ToString();
								var newUser = Newtonsoft.Json.JsonConvert.DeserializeObject<UserFB>(s);

								if (newUser != null)
								{
									//await DisplayAlert("Usuario", "Usuario creado con exito", "Aceptar");
									GeneralSetting.UserSettings = JsonConvert.SerializeObject(newUser);
									App.NavigateToProfile();
									Loading(false);



								}

							}
							else {
								await DisplayAlert("Usuario", responseApi["mensaje"].ToString(), "Aceptar");

							}

						}
						else {
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


		async void SignIn_Clicked(object sender, System.EventArgs e)
		{
			await Navigation.PushAsync(new LoginPage());
		}

		async void TerminosYCondiciones_Clicked(object sender, System.EventArgs e)
		{
			//string url = "https://www.datospersonales.gub.uy/wps/wcm/connect/urcdp/57e2264e-370f-411d-933c-63aca968c88b/Clausula_de_Consentimiento_para_Organismos_Publicos.pdf?MOD=AJPERES&CONVERT_TO=url&CACHEID=57e2264e-370f-411d-933c-63aca968c88b";
			//await Navigation.PushAsync(new PdfViewPage(url)); 

			if (!String.IsNullOrEmpty(App.configuracion.urlTerminosYCondiciones)){ 
					await Navigation.PushAsync(new PdfViewPage(App.configuracion.urlTerminosYCondiciones)); 
			}

		}




    }
}
