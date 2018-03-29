using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IDD
{
    public partial class NoticiasPage : ContentPage
    {
        NoticiaService _noticiaService;
        public NoticiasPage()
        {
            InitializeComponent();

            _noticiaService = new NoticiaService();

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
				var noticias = await _noticiaService.GetNoticias();
				Console.WriteLine("OK listas");
				Device.BeginInvokeOnMainThread(() =>
				{
					if (noticias != null)
                        listNoticias.CargarLista(noticias);
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



        List<Noticia> GetSample()
        {
            var list = new List<Noticia>();

            list.Add(new Noticia{
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

			list.Add(new Noticia
			{
				id = 5,
				imagen = "https://cuponassets.cuponatic-latam.com/backendCl/uploads/imagenes_descuentos/106529/eba18c6f06f0957039f1790c78a676c787871ee4.XL.jpg",
				titulo = "Lorem ipsum dolor sit amet, consectetur cras amet."
			});

			list.Add(new Noticia
			{
				id = 5,
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
    }
}
