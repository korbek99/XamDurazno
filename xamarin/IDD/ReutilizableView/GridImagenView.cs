using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace IDD
{
    public class GridImagenView : Grid
    {
		public static readonly BindableProperty IsReadOnlyProperty = BindableProperty.Create("IsReadOnly", typeof(bool), typeof(GridImagenView), false);
        public CrearReclamoPage pageCrear;

		public bool IsReadOnly
		{
			get
			{
				return (bool)GetValue(IsReadOnlyProperty);
			}

			set
			{
				SetValue(IsReadOnlyProperty, value);
			}
		}
		Grid gridImagenes;
		public List<MatrizImagen> listMatriz;
		Label label;
		TapGestureRecognizer tapEliminarImagen;
		int countImagen;
        Image imgPrincipal;

		public ContentPage page;

		protected override void OnParentSet()
		{
			base.OnParentSet();

			RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            RowDefinitions.Add(new RowDefinition { Height = new GridLength(45) });
            RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });

            imgPrincipal = new Image
            {
                Source = "ic_reporte_3_sin_foto",
                HeightRequest=80,
                WidthRequest=80,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };

            Children.Add(imgPrincipal, 0, 0);


			label = new Label
			{
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Text = "Toca una imagen para eliminarla",
				FontSize = 12,
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalTextAlignment = TextAlignment.Center,
				TextColor = Color.Gray
			};

			Children.Add(label, 0, 1);

			var gridOpciones = new Grid
			{
				VerticalOptions = LayoutOptions.EndAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				ColumnSpacing = 2,
				BackgroundColor = (Color)Application.Current.Resources["AzulColor"]
			};
			gridOpciones.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
			gridOpciones.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2) });
			gridOpciones.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

			var imgGaleria = new Image
			{
				Source = "ic_reporte_galeria",
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Aspect = Aspect.AspectFit,
				Margin = new Thickness(0, 5, 0, 0)
			};

			var lblGaleria = new Label
			{
				Text = "Galeria",
				TextColor = Color.White,
				FontSize = 10,
				HorizontalTextAlignment = TextAlignment.Center
			};

			var stackGaleria = new StackLayout();
			stackGaleria.Children.Add(imgGaleria);
			stackGaleria.Children.Add(lblGaleria);
			gridOpciones.Children.Add(stackGaleria, 2, 0);

			TapGestureRecognizer tapGaleria = new TapGestureRecognizer();
			tapGaleria.Tapped += async (object sender, EventArgs e) => { await CameraGetMediaFile("Galería"); };
			stackGaleria.GestureRecognizers.Add(tapGaleria);

			var separador = new BoxView
			{
				BackgroundColor = Color.Gray,
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand
			};
			gridOpciones.Children.Add(separador, 1, 0);

			var imgCamara = new Image
			{
				Source = "ic_reporte_camara",
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Aspect = Aspect.AspectFit,
				Margin = new Thickness(0, 5, 0, 0)
			};

			var lblCamara = new Label
			{
				Text = "Cámara",
				TextColor = Color.White,
				FontSize = 10,
				HorizontalTextAlignment = TextAlignment.Center
			};

			var stackCamara = new StackLayout();
			stackCamara.Children.Add(imgCamara);
			stackCamara.Children.Add(lblCamara);
			gridOpciones.Children.Add(stackCamara, 0, 0);
			TapGestureRecognizer tapCamara = new TapGestureRecognizer();
			tapCamara.Tapped += async (object sender, EventArgs e) => { await CameraGetMediaFile("Cámara"); };
			stackCamara.GestureRecognizers.Add(tapCamara);

			//stackOpciones.Children.Add(gridOpciones);
			Children.Add(gridOpciones, 0, 2);
		}

		public GridImagenView()
		{
			listMatriz = MatrizImagen.CrearMatriz(2, 3);

		}

		public void AddImagen(Foto foto)
		{
			PrepararGrid();

			foreach (var i in listMatriz)
			{
				if (!i.usada)
				{
                    var img = GetImagen(foto.url);
					gridImagenes.Children.Add(img, i.column, i.row);
					countImagen++;
					img.idImg = countImagen;
					i.idImg = countImagen;
					i.usada = true;
					break;
				}
			}
		}

		public void AddImagen(MediaFile file)
		{
			if (file == null) return;
			if (listMatriz.Where(x => x.usada).ToList().Count == 6)
			{
				if (page != null)
				{
					Device.BeginInvokeOnMainThread(async () =>
					{
						await page.DisplayAlert("Multimedia", "Solo se permite un máximo de 6 imágenes.", "Aceptar");
					});
				}
				return;
			}

			PrepararGrid();

			foreach (var i in listMatriz)
			{
				if (!i.usada)
				{
					i.bytes = CamaraHelper.GetByteFromMediaFile(file);
					var img = GetImagen(file.Path);
					gridImagenes.Children.Add(img, i.column, i.row);
					countImagen++;
					img.idImg = countImagen;
					i.idImg = countImagen;
					i.usada = true;
					img.GestureRecognizers.Add(tapEliminarImagen);
					break;
				}
			}
		}

		Imagen GetImagen(string path)
		{
			return new Imagen
			{
				VerticalOptions = LayoutOptions.StartAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Source = path,
				Aspect = Aspect.AspectFit
			};
		}

		void PrepararGrid()
		{
			//if (listByte == null)
			if (gridImagenes == null)
			{
                
				tapEliminarImagen = new TapGestureRecognizer();
				tapEliminarImagen.Tapped += TapEliminarImagen_Tapped;

				gridImagenes = new Grid
				{
					VerticalOptions = LayoutOptions.StartAndExpand,
					HorizontalOptions = LayoutOptions.FillAndExpand,
					HeightRequest = 175,
					Margin = new Thickness(20, 10)
				};
                gridImagenes.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
				gridImagenes.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                gridImagenes.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                gridImagenes.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
				gridImagenes.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
				gridImagenes.RowDefinitions.Add(new RowDefinition { Height = new GridLength(25, GridUnitType.Absolute) });

				
				Children.Add(gridImagenes, 0, 0);

                Children.Remove(imgPrincipal);
			}

			if (gridImagenes.Children.Count > 6) return;
		}

		async void TapEliminarImagen_Tapped(object sender, EventArgs e)
		{
            var action = await page.DisplayActionSheet("¿Seguro que desea eliminar la imagen?", "Cancelar", null, "Si", "No");
			if (action == "Si")
			{
				var img = sender as Imagen;

				gridImagenes.Children.Remove(img);
				listMatriz.Where(x => x.idImg == img.idImg).FirstOrDefault().usada = false;
				listMatriz.Where(x => x.idImg == img.idImg).FirstOrDefault().bytes = null;

				var r = listMatriz.Where(x => x.usada == true).ToList().Count;

				if (listMatriz.Where(x => x.usada == true).ToList().Count == 0)
				{
					LimpiarGrilla();
				}
			}


		}

		public void LimpiarGrilla()
		{
			Children.Remove(gridImagenes);
			gridImagenes = null;
            Children.Add(imgPrincipal, 0, 0);
		}

		async Task CameraGetMediaFile(string action)
		{
	
			pageCrear.isPhoto = true;
			if (action == "Cámara")
			{
				try
				{
					if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
					{
						await page.DisplayAlert("Camara", ":( No hay camara disponible.", "OK");
					}

					AddImagen(await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
					{
						Directory = "perfil_SV",
						PhotoSize = PhotoSize.Medium
					}));
				}
				catch (Exception ee)
				{
					await page.DisplayAlert("error", ee.Message, "OK");
				}

			}
			else if (action == "Galería")
			{
				if (!CrossMedia.Current.IsPickPhotoSupported)
				{

					await page.DisplayAlert("Sin permisos", ":( No tenemos los permisos para entrar a tu libreria.", "OK");
				}

                Device.BeginInvokeOnMainThread(async () => { AddImagen(await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions() { PhotoSize = PhotoSize.Medium })); });
			}
		}
	}

	public class MatrizImagen
	{
		public int row
		{
			get;
			set;
		}

		public int column
		{
			get;
			set;
		}

		public int idImg
		{
			get;
			set;
		}

		public bool usada
		{
			get;
			set;
		}

		public byte[] bytes
		{
			get;
			set;
		}

		public MatrizImagen(int rowIn, int colIn)
		{
			row = rowIn;
			column = colIn;
		}

		public static List<MatrizImagen> CrearMatriz(int totalRow, int totalCol)
		{
			var list = new List<MatrizImagen>();
			for (int i = 0; i < totalRow; i++)
			{
				for (int e = 0; e < totalCol; e++)
				{
					var img = new MatrizImagen(i, e);

					list.Add(img);
				}
			}


			return list;
		}
	}

}
