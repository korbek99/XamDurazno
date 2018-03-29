using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Threading.Tasks;

namespace IDD
{
    public partial class CampanasPage : ContentPage
    {
        CampanaService _campanaService;

		public CampanasPage()
		{
			InitializeComponent();

			_campanaService = new CampanaService();
            listCampana.ItemSelected += ListView_ItemSelected;
            //listCampana.ItemsSource = GetTest();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
            Loading(true);
			LoadMain();
			//listNoticias.ItemsSource = GetSample();
		}

        async Task LoadMain()
        {
            await Task.Run(async () =>
            {
                var campanas = await _campanaService.GetCampanas(App.userFB.id);
                Console.WriteLine("OK listas ");
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (campanas != null)
                        listCampana.ItemsSource = campanas;
                    Loading(false);
                });

            });
        }

		async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var item = e.SelectedItem as Campana;
			listCampana.SelectedItem = null;
			if (item != null)
			{
                await Navigation.PushAsync(new DetalleCampanaPage(item));
			}
		}

		void Loading(bool isVisible)
		{
			boxIndicator.IsVisible = isVisible;
			actIndicator.IsRunning = isVisible;
			actIndicator.IsVisible = isVisible;
		}

        public List<Campana> GetTest(){
            List<Campana> list = new List<Campana>();

            list.Add(new Campana
            {
				id = 1,
				titulo = "titulo campaña 1",
				descripcion = "campaña 1",
				foto_url = "https://s3.amazonaws.com/iddurazno/campana/7b82db34e65f746e9fe056a4872f53e5.jpg",
				likes = 0,
				cantidad_comentarios = 0
            });

			list.Add(new Campana
			{
				id = 1,
				titulo = "titulo campaña 1",
				descripcion = "campaña 1",
				foto_url = "https://s3.amazonaws.com/iddurazno/campana/7b82db34e65f746e9fe056a4872f53e5.jpg",
				likes = 0,
				cantidad_comentarios = 0
			});

			list.Add(new Campana
			{
				id = 1,
				titulo = "titulo campaña 1",
				descripcion = "campaña 1",
				foto_url = "https://s3.amazonaws.com/iddurazno/campana/7b82db34e65f746e9fe056a4872f53e5.jpg",
				likes = 0,
				cantidad_comentarios = 0
			});

            return list;
        }
    }
}
