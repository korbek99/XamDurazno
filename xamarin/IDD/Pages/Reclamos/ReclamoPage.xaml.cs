using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Share;
using Plugin.Share.Abstractions;
using Xamarin.Forms;

namespace IDD
{
    public partial class ReclamoPage : ContentPage
    {

		Reclamo reclamo = new Reclamo();

		TapGestureRecognizer tapPdf;
		TapGestureRecognizer tapCompartir;

		string imagenAntes = "";
		string imagenDespues = "";
		string imagenCompartir = "";

        public ReclamoPage()
        {
            InitializeComponent();
            Loading(false);
        }

        public ReclamoPage(Reclamo _reclamo)
		{
			InitializeComponent();

			tapPdf = new TapGestureRecognizer();
			tapPdf.Tapped += async (object sender, EventArgs e) => { await Navigation.PushAsync(new PdfViewPage(reclamo.pdf)); };
			slPdf.GestureRecognizers.Add(tapPdf);


			tapCompartir = new TapGestureRecognizer();
			tapCompartir.Tapped += TapCompartir_Tapped;
			slCompartir.GestureRecognizers.Add(tapCompartir);


			reclamo = _reclamo;
			if (reclamo.imagenes.Length > 0) {
				Loading(true);
				cargarImagenes();
			}

		}

		///imagen_reclamo_compartir
		async Task LoadImagenCompartir()
		{
			await Task.Run(async () =>
			{
				var data = await ReclamoService.GetImagenParaCompartir(imagenAntes, imagenDespues);

                var datos = await ReclamoService.GetImagenParaCompartirData(imagenAntes, imagenDespues , data);

				Console.WriteLine("OK Imagen");
				Device.BeginInvokeOnMainThread(() =>
				{
                    if (data != null)
                        //imagenCompartir = data;
                        imagenCompartir =  datos;

					if (!String.IsNullOrEmpty(imagenCompartir))
					{
						if (CrossShare.IsSupported)
						{
							CrossShare.Current.Share(new ShareMessage
							{
								Text = reclamo.mensajeCompartir,
								Url = imagenCompartir
							},
						   new ShareOptions
						   {
							   ChooserTitle = null,
							   ExcludedUIActivityTypes = new[] {
									ShareUIActivityType.AirDrop,
									//ShareUIActivityType.Mail,
									ShareUIActivityType.OpenInIBooks,
									//ShareUIActivityType.PostToWeibo,
									ShareUIActivityType.PostToFlickr,
									ShareUIActivityType.CopyToPasteboard,
									//ShareUIActivityType.PostToTencentWeibo,
									//ShareUIActivityType.PostToVimeo,
									ShareUIActivityType.Print,
									//ShareUIActivityType.SaveToCameraRoll,
									ShareUIActivityType.AddToReadingList
							   }
						   }); //new share options

						}
					}
					Loading(false);
				});

			});		
		
		}



		async void TapCompartir_Tapped(object sender, EventArgs e)
		{
			Loading(true);
			await LoadImagenCompartir();
		}



		void cargarImagenes()
		{

			var antesConImagen = false;
			var despuesConImagen = false;

			Array.Reverse(reclamo.imagenes);
			foreach (var imagen in reclamo.imagenes)
			{
				if (imagen.esDespues == false && antesConImagen == false)
				{
					imgAntes.Source = imagen.url;
					imagenAntes = imagen.url;
					antesConImagen = true;
				}
				else if (imagen.esDespues == true && despuesConImagen == false)
				{

					imgDespues.Source = imagen.url;
					imagenDespues = imagen.url;
					despuesConImagen = true;
				}
			}

			Loading(false);

		}


		void Loading(bool isVisible)
		{
			boxIndicator.IsVisible = isVisible;
			actIndicator.IsRunning = isVisible;
			actIndicator.IsVisible = isVisible;
		}

    }
}
