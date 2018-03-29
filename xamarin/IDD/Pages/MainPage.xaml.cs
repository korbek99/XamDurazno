using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;



namespace IDD
{
    //[Android.App.Activity(Label = "IDD.Droid", Icon = "@drawable/icon_idd", Theme = "@style/ConLogo")]
    public partial class MainPage : ContentPage
	{

		TapGestureRecognizer tapReclamar;

        NoticiaService _noticiaService;
		public MainPage()
		{
			InitializeComponent();

			tapReclamar = new TapGestureRecognizer();
			tapReclamar.Tapped += async (object sender, EventArgs e) => {


				if (String.IsNullOrEmpty(GeneralSetting.UserSettings))
				{
					var alertResult = await DisplayAlert("Atención!", "Para crear reclamos debe registrarse ¿desea continuar?", "No", "Si");
					if (!alertResult)
					{
						await Navigation.PushAsync(new RegistroPage());
					}
				}
				else { 
					await Navigation.PushAsync(new CrearReclamoPage());
				}




			
			
			};

			stkReclama.GestureRecognizers.Add(tapReclamar);


            _noticiaService = new NoticiaService();
            listNoticias.ItemSelected += ListNoticias_ItemSelected;




            if (Device.OS != TargetPlatform.iOS)
            {

                //this.ToolbarItems.Add(logo);
                this.Title = "Home";

                //NavigationPage.SetTitleIcon(this, "disfruta.png");
            }
            else
            {

                this.Title = "Home";
                //NavigationPage.SetTitleIcon(this, "disfruta.png");

            }

		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
      
            Loading(true);
            LoadMain();

		}


		async Task LoadMain()
		{
			await Task.Run(async () =>
			{
                var noticias = await _noticiaService.GetNoticias(true);
				Console.WriteLine("OK listas");
				Device.BeginInvokeOnMainThread(() =>
				{
                    if (noticias != null)
                        listNoticias.ItemsSource = noticias;
					Loading(false);
				});

			});
		}


        async void ListNoticias_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var noticia = e.SelectedItem as Noticia;
            if(noticia != null)
            {
                await App.Navigator.PushAsync(new DetalleNoticiaPage(noticia));  
            }
        }

        async void Pagos_Tapped(object sender, System.EventArgs e)
        {
            await App.Navigator.PushAsync(new CalendarioPagoPage());
        }

		async void Noticia_Tapped(object sender, System.EventArgs e)
		{
            await App.Navigator.PushAsync(new NoticiasPage());
		}

        List<Noticia> GetSample()
		{
			var list = new List<Noticia>();

			list.Add(new Noticia
			{
				id = 1,
				imagen = "https://cuponassets.cuponatic-latam.com/backendCl/uploads/imagenes_descuentos/106529/eba18c6f06f0957039f1790c78a676c787871ee4.XL.jpg",
				titulo = "Lorem ipsum dolor sit amet, consectetur cras amet."
			});

			list.Add(new Noticia
			{
				id = 2,
				imagen = "https://cuponassets.cuponatic-latam.com/backendCl/uploads/imagenes_descuentos/106529/eba18c6f06f0957039f1790c78a676c787871ee4.XL.jpg",
				titulo = "Lorem ipsum dolor sit amet, consectetur cras amet."
			});

			list.Add(new Noticia
			{
				id = 3,
				imagen = "https://cuponassets.cuponatic-latam.com/backendCl/uploads/imagenes_descuentos/106529/eba18c6f06f0957039f1790c78a676c787871ee4.XL.jpg",
				titulo = "Lorem ipsum dolor sit amet, consectetur cras amet."
			});

			list.Add(new Noticia
			{
				id = 4,
				imagen = "https://cuponassets.cuponatic-latam.com/backendCl/uploads/imagenes_descuentos/106529/eba18c6f06f0957039f1790c78a676c787871ee4.XL.jpg",
				titulo = "Lorem ipsum dolor sit amet, consectetur cras amet."
			});

			list.Add(new Noticia
			{
				id = 5,
				imagen = "https://cuponassets.cuponatic-latam.com/backendCl/uploads/imagenes_descuentos/106529/eba18c6f06f0957039f1790c78a676c787871ee4.XL.jpg",
				titulo = "Lorem ipsum dolor sit amet, consectetur cras amet."
			});


			return list;
		}

		void Loading(bool isVisible)
		{
			boxIndicator.IsVisible = isVisible;
			actIndicator.IsRunning = isVisible;
			actIndicator.IsVisible = isVisible;
		}
	}
}
