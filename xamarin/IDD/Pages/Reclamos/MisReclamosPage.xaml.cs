using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IDD
{
    public partial class MisReclamosPage : ContentPage
    {
        TapGestureRecognizer tapReclamar;
        public MisReclamosPage()
        {
            InitializeComponent();

            tapReclamar = new TapGestureRecognizer();
            tapReclamar.Tapped += async (object sender, EventArgs e) => { await Navigation.PushAsync(new CrearReclamoPage()); };

            stkReclama.GestureRecognizers.Add(tapReclamar);

          //  listReclamos.ItemsSource = GetSample();

            listReclamos.ItemSelected += ListReclamos_ItemSelected;

            if (string.IsNullOrEmpty(GeneralSetting.UserSettings))
            {
                stkReclamos.IsVisible = true;
                listReclamos.IsVisible = false;
            }
            else
            {
                stkReclamos.IsVisible = false;
                listReclamos.IsVisible = true;
            }
            Loading(false);
        }


		protected override void OnAppearing()
		{
			base.OnAppearing();
			Loading(true);
			LoadMain();
		}


        async void ListReclamos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var reclamo = e.SelectedItem as Reclamo;
            if(reclamo !=  null)
            {
				if (reclamo.IsFinalizado)
				{
					await Navigation.PushAsync(new ReclamoPage(reclamo));
				}
				else { 
					 await Navigation.PushAsync(new PdfViewPage(reclamo.pdf));
				}
            }
        }

		async Task LoadMain()
		{
			await Task.Run(async () =>
			{
				var data = await UserService.GetReclamos(App.userFB.email);

				Console.WriteLine("OK listas");
				Device.BeginInvokeOnMainThread(() =>
				{
					if (data != null)
						listReclamos.ItemsSource = data;
					Loading(false);
				});

			});
		}



        //async Task LoadMain()
        //{
        //	await Task.Run(async () =>
        //	{
        //		var noticias = await _noticiaService.GetNoticias(true);
        //		Console.WriteLine("OK listas");
        //		Device.BeginInvokeOnMainThread(() =>
        //		{
        //			if (noticias != null)
        //				listNoticias.ItemsSource = noticias;
        //			Loading(false);
        //		});

        //	});
        //}

        void Loading(bool isVisible)
		{
			boxIndicator.IsVisible = isVisible;
			actIndicator.IsRunning = isVisible;
			actIndicator.IsVisible = isVisible;
		}

        public List<Reclamo> GetSample()
        {
            var list = new List<Reclamo>();

            list.Add(new Reclamo()
            {
                id = 0,
                titulo = "t1",
                fechaCreacion = "20-01-17",
                estado = "Finalizado"
            });

			list.Add(new Reclamo()
			{
				id = 0,
				titulo = "t2",
				fechaCreacion = "20-01-17",
				estado = "Estado"
			});

			list.Add(new Reclamo()
			{
				id = 0,
				titulo = "t3",
				fechaCreacion = "20-01-17",
				estado = "Estado"
			});

            return list;
        }
    }
}
