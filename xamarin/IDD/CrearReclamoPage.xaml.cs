using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IDD
{
    public partial class CrearReclamoPage : ContentPage
    {
        MapTapRenderer map;
        TapGestureRecognizer tapCurrent;
        TapGestureRecognizer tapEnviar;
        ReclamoService _reclamoService;
        public bool isPhoto;

        public CrearReclamoPage()
        {
            InitializeComponent();

            stackMultimediaCausa.page = this;
            tapCurrent = new TapGestureRecognizer();

#if __IOS__
            tapCurrent.Tapped += async (object sender, EventArgs e) => { await map.GetCurrent(); };
#endif
#if __ANDROID__
			tapCurrent.Tapped += (sender, e) => { map.OnGetCurrentEvent(sender, e); };
#endif
            imgCurrent.GestureRecognizers.Add(tapCurrent);


            tapEnviar = new TapGestureRecognizer();
            tapEnviar.Tapped += TapEnviar_Tapped;

            stkEnviar.GestureRecognizers.Add(tapEnviar);

            editorDescription.TextChanged += EditorDescription_TextChanged;
            txtTitulo.TextChanged += Titulo_TextChanged;

            _reclamoService = new ReclamoService();
            stackMultimediaCausa.pageCrear = this;

            txtTelefono.TextChanged += GlobalHelpers.OnEntryTextChanged;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!isPhoto) Loading(true);
            if(!isPhoto) LoadMain();
            //LoadMain();
        }

		async Task LoadMain()
		{
			await Task.Run(() =>
			{

                //if(App.userFB != null)
                //{
                //    txtNombre.Text = App.userFB.first_name + " " + App.userFB.last_name;
                //    txtMail.Text = App.userFB.email;
                //    txtTelefono.Text = App.userFB.phone;
                //}
				if (map == null)
				{
					map = new MapTapRenderer
					{
						MapType = Xamarin.Forms.Maps.MapType.Street,
						IsShowingUser = true,
						//denunciaPAge = this
					};

				}

				Device.BeginInvokeOnMainThread(() =>
				{
                    gMap.Children.Add(map, 0, 0);
					Loading(false);
				});

			});
		}

        void Uno_Clicked(object sender, System.EventArgs e)
        {
            imgTab.Source = "ic_reporte_1";

            gMap.IsVisible = true;
            imgCurrent.IsVisible = true;
            gDatos.IsVisible = false;
            stackMultimediaCausa.IsVisible = false;
        }

		void Dos_Clicked(object sender, System.EventArgs e)
		{
			imgTab.Source = "ic_reporte_2";

            gMap.IsVisible = false;
            imgCurrent.IsVisible = false;
			gDatos.IsVisible = false;
            stackMultimediaCausa.IsVisible = true;
		}

		async void Tres_Clicked(object sender, System.EventArgs e)
		{
            if(stackMultimediaCausa.listMatriz.Where(x => x.usada).ToList().Count > 0)
            {
                Loading(true);
				if(App.userFB != null)
				{

					var full_name = String.IsNullOrEmpty(App.userFB.full_name) ? App.userFB.first_name + " " + App.userFB.last_name : App.userFB.full_name;
					txtNombre.Text = full_name;
				    txtMail.Text = App.userFB.email;
				    txtTelefono.Text = App.userFB.phone;
				}
                Loading(false);

				imgTab.Source = "ic_reporte_3";

				gMap.IsVisible = false;
				imgCurrent.IsVisible = false;
				gDatos.IsVisible = true;
				stackMultimediaCausa.IsVisible = false;
            }
            else
            {
                await DisplayAlert("¡Uh oh!", "Debes sacar por lo menos una foto para tu reclamo.", "Aceptar");
            }
			
		}

        async void TapEnviar_Tapped(object sender, EventArgs e)
        {
            var pasa_validacion = true;
			if(!string.IsNullOrEmpty(txtTitulo.Text) && !string.IsNullOrEmpty(editorDescription.Text))
            {
                //si viene el mail entonces validar 
                if(!string.IsNullOrEmpty((txtMail.Text)))
                {
                    if (!GlobalHelpers.EmailBienEscrito(txtMail.Text))
                    {
                        await DisplayAlert("¡Uh oh!", "Email no valido.", "Aceptar");
                        pasa_validacion = false;
                    }
                }
                if (pasa_validacion){
                    
					Loading(true);
					var r = await _reclamoService.CrearReclamo(txtTitulo.Text,
												editorDescription.Text,
												txtNombre.Text,
												txtMail.Text,
												txtTelefono.Text,
												map.coordinate.Latitude.ToString(),
												map.coordinate.Longitude.ToString(),
												stackMultimediaCausa.listMatriz.Where(x => x.usada).Select(x => x.bytes).ToList(),
												txtReferencia.Text);
					Loading(false);

					await DisplayAlert("Reclamo enviado", r, "Aceptar");

					//await Navigation.PushAsync(new MisReclamosPage());
					App.NavigateToProfile();

                }
					
			}else
			{
			    await DisplayAlert("¡Uh oh!", "Título y descripción son obligatorios para realizar tu reclamo.", "Aceptar");
			}
		}

		void EditorDescription_TextChanged(object sender, TextChangedEventArgs e)
		{
			string _text = editorDescription.Text;      //Get Current Text
			if (_text != null)
			{
				if (_text.Length > 1024)       //If it is more than your character restriction
				{
					_text = _text.Remove(_text.Length - 1);  // Remove Last character
					editorDescription.Text = _text;        //Set the Old value
				}
			}
		}

		void Titulo_TextChanged(object sender, TextChangedEventArgs e)
		{
			string _text = editorDescription.Text;      //Get Current Text
            if(_text != null)
            {
				if (_text.Length > 100)       //If it is more than your character restriction
				{
					_text = _text.Remove(_text.Length - 1);  // Remove Last character
					editorDescription.Text = _text;        //Set the Old value
				}
            }
		}

        void Loading(bool isVisible)
		{
			boxIndicator.IsVisible = isVisible;
			actIndicator.IsRunning = isVisible;
			actIndicator.IsVisible = isVisible;
		}
    }
}
