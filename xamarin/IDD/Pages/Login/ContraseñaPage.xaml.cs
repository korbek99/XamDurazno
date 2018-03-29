using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace IDD
{
    public partial class ContraseñaPage : ContentPage
    {

        UserFB user;

		TapGestureRecognizer tapTerminar;


		UserService _userService;
        FbService _fbService;


        public ContraseñaPage()
        {
            InitializeComponent();
        }

        public ContraseñaPage(UserFB _user){

            InitializeComponent();
		
			this.user = _user;
			_userService = new UserService();

			tapTerminar = new TapGestureRecognizer();
			tapTerminar.Tapped +=   TapTerminar_Tapped;
			johnnSl.GestureRecognizers.Add(tapTerminar);


			 Loading(false);

		}

        void TapTerminar_Tapped(object sender, EventArgs e)
        {
            var pasa_validacion = true;

            if (!string.IsNullOrEmpty(txtPassword.Text) && !string.IsNullOrEmpty(txtRepeatPassword.Text))
			{
                string password = txtPassword.Text.Trim();
                string repeatPassword = txtRepeatPassword.Text.Trim();

                if(password != repeatPassword){

                     DisplayAlert("¡Uh oh!", "Contraseñas no coinciden.", "Aceptar");
                    pasa_validacion = false;
                }

                if(password.Length < 4){

                     DisplayAlert("¡Uh oh!", "Contraseña muy corta. minimo 4 caracteres", "Aceptar");
                    pasa_validacion = false;
                }

                if(pasa_validacion){            
					user.password = txtPassword.Text;

                    Loading(true);
                    Registrar();
                } 
            }
            else
            {
                 DisplayAlert("¡Uh oh!", "Todos los datos son obligatorios.", "Aceptar");

            }
        }


		public async Task Registrar()
		{
			Console.WriteLine("entra a registrar");

			await Task.Run(async () =>
			{

				try {
					
					var responseApi = await _userService.PostUser(user);



					Device.BeginInvokeOnMainThread(async () =>
					{
						Loading(false);
						if(responseApi != null) {
							
							if (responseApi["codigo"].ToString() == "1")
							{
								var s = responseApi["data"].ToString();
								var newUser = Newtonsoft.Json.JsonConvert.DeserializeObject<UserFB>(s);

								if(newUser != null){
									
									GeneralSetting.UserSettings = JsonConvert.SerializeObject(newUser);
									App.NavigateToProfile();

								}

							}else {

									await DisplayAlert("Usuario", responseApi["mensaje"].ToString(), "Aceptar");

							}


						}else {

							await DisplayAlert("Usuario","Ha ocurrido un error", "Aceptar");
						}
					});

				} catch (Exception e) {
					Loading(false);
					await DisplayAlert("Usuario","Ha ocurrido un error " + e.Message, "Aceptar");
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
