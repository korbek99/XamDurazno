using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IDD
{
    public partial class DetalleNoticiaPage : ContentPage
    {
        TapGestureRecognizer tap;
        Noticia n;
        NoticiaService _noticiaService;
		string detalleNoticia = "";


        public DetalleNoticiaPage()
        {
            InitializeComponent();
            tap = new TapGestureRecognizer();
            tap.Tapped += Tap_Tapped;
            _noticiaService = new NoticiaService();
        }

        public DetalleNoticiaPage(Noticia noticia)
		{
			InitializeComponent();
            n = noticia;
			tap = new TapGestureRecognizer();
			tap.Tapped += Tap_Tapped;
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
                var noticia = await _noticiaService.GetNoticia(n.id);
				Console.WriteLine("OK listas");
				Device.BeginInvokeOnMainThread(() =>
				{
					if (noticia != null)
                    {
                        n = noticia;


						JustifyText(n.descripcion);

						string imagenYoutube = string.IsNullOrEmpty(n.imgYoutube.Trim()) ? n.imagen : n.imgYoutube;
						if (n.urlYoutube.Contains("channel"))
							imagenYoutube = n.imagen;

						Console.WriteLine(imagenYoutube);
						Console.WriteLine(n.imagen);

						imgYoutube.Source = imagenYoutube;

						//si la imagen  de youtube esta vacia no mostrar el play
                        if (string.IsNullOrEmpty(n.imgYoutube)) imgPlay.IsVisible = false;


                        if(n.imagenes.Count > 0)
                        {
							imgPrincipal.Source = n.imagenes[0].url;
							imgPrincipal.id = n.imagenes[0].id;
							imgPrincipal.IsVisible = true;
                        }

						var grid = new Grid();
						grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
						grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
						grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
						grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

						//agregar imagenes al carousel
						int column = 0;
                        foreach (var i in n.imagenes)
						{
							if (i.id != imgPrincipal.id)
							{
								var img = new ImageLocal
								{
									id = i.id,
									Source = i.url,
									Aspect = Aspect.AspectFill
								};
								img.GestureRecognizers.Add(tap);
								grid.Children.Add(img, column, 0);
								column++;
							}
						}
						gridPrincipal.Children.Add(grid, 0, 3);
                    }
                    //JustifyText(n.descripcion);
					Loading(false);

				});

			});
		}


		  void JustifyText(string text) { 
			
			var source = new HtmlWebViewSource();
			var textHtml = "<html>" +
				 "<head>"+
                      "\t\n<meta name='viewport' content='width=device-width; height=device-height; initial-scale=1.0; maximum-scale=1.0; user-scalable=0;'/>"+
                "</head>"+
				"<body  style=\"text-align:justify;text-justify: inter-word;font-family:Arial;padding:0px;line-height:22px;\">" +
					"</body>" + text + "</html>";
			source.Html = textHtml ;
			 webViewLayout.Source = source;

		}
        void Tap_Tapped(object sender, EventArgs e)
        {
            var img = sender as ImageLocal;
            if(img != null)
            {
                var principal = n.imagenes.Where(x => x.id == imgPrincipal.id).FirstOrDefault();

                imgPrincipal.Source = img.Source;
                imgPrincipal.id = img.id;

                img.Source = principal.url;
                img.id = principal.id;
            }
        }

        async void Handle_Tapped(object sender, System.EventArgs e)
        {
            if (!String.IsNullOrEmpty(n.urlYoutube))
            {
                await App.Navigator.PushAsync(new WebViewPage(n.urlYoutube));
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
