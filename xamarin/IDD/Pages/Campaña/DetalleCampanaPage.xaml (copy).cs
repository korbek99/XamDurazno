using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IDD
{
    public partial class DetalleCampanaPage : ContentPage
    {
        TapGestureRecognizer tapLike;
		//TODO:falta imagen de mas informacion y pdf url entregada por la intendencia por confirmar
        TapGestureRecognizer tapPdf; //mas informacion 

        TapGestureRecognizer tapComentar;
        CampanaService _campanaService;

        Campana campana;
        public DetalleCampanaPage()
        {
            InitializeComponent();
        }


		protected override void OnAppearing()
		{
			base.OnAppearing();

			if(campana != null) 
				lblComentario.Text = "Comentarios: " + campana.cantidad_comentarios.ToString();	
			
		}


		public DetalleCampanaPage(Campana campana)
		{
			InitializeComponent();

            this.campana = campana;

            _campanaService = new CampanaService();

            lblLike.Text = "Likes: "+campana.likes.ToString();
            lblComentario.Text = "Comentarios: " + campana.cantidad_comentarios.ToString();
            lblDescripcion.Text = campana.descripcion;
            imgPrincipal.Source = campana.foto_url;

            tapLike = new TapGestureRecognizer();
            tapLike.Tapped += TapLike_Tapped;

            imgLike.GestureRecognizers.Add(tapLike);

            tapPdf = new TapGestureRecognizer();
            tapPdf.Tapped += async (object sender, EventArgs e) => { 
				await Navigation.PushAsync(new PdfViewPage(campana.pdf_url)); 
			};

            imgPdf.GestureRecognizers.Add(tapPdf);


			if (String.IsNullOrEmpty(GeneralSetting.UserSettings))
			{
				stkComentar.IsVisible = false;

			}
			else {
				
				stkComentar.IsVisible = true;
			}



			tapComentar = new TapGestureRecognizer();
			tapComentar.Tapped += async (object sender, EventArgs e) => { await Navigation.PushAsync(new ComentarioPage(campana)); };

            stkComentar.GestureRecognizers.Add(tapComentar);

            Loading(false);
		}

        async void TapLike_Tapped(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(GeneralSetting.UserSettings))
            {
					var alertResult = await DisplayAlert("Atención!", "Para darle like debe registrarse ¿desea continuar?", "No", "Si");
					if (!alertResult)
					{
						await Navigation.PushAsync(new RegistroPage());
					}
            }
            else
            {
				if (campana.id != null && App.userFB.id != null) { 
                     Loading(true);
	   				 Like();
				}
               
            }



        }

		public async Task Like()
		{
			await Task.Run(async () =>
			{
				var mnsj = await _campanaService.Like(campana.id, App.userFB.id);
				Device.BeginInvokeOnMainThread(async () =>
				{
					//	await DisplayAlert("Campaña", mnsj, "Aceptar");
					campana.likes = (mnsj == "Like creado exitosamente") ? (campana.likes + 1) : (campana.likes - 1);
					lblLike.Text = "Likes: " + campana.likes.ToString();
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
