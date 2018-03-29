using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace IDD
{
    public class ListaNoticias : ScrollView
    {
        TapGestureRecognizer tap;
        List<Noticia> noticias;
        public ListaNoticias()
        {
            VerticalOptions = LayoutOptions.StartAndExpand;
            tap = new TapGestureRecognizer();
            tap.Tapped += Tap_Tapped;
        }

        public void CargarLista(List<Noticia> noticias)
        {
            this.noticias = noticias;
            int row = 0;
            int column = 0;
            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition(){ Width = new GridLength(1, GridUnitType.Star)});
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnSpacing = 10;
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(160) });

            foreach(var i in noticias)
            {
                if(column == 2)
                {
                    grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(160) });
                    row++;
                    column = 0;
                }

                var stack = new StackLocal { id = i.id };
                stack.GestureRecognizers.Add(tap);

                var img = new Image { HeightRequest = 100, Aspect = Aspect.AspectFill, Source = i.imagen };
                stack.Children.Add(img);

                var label = new Label 
                { 
                    HeightRequest = 50, 
                    Margin = new Thickness(10, 0), 
                    Text = i.titulo, 
                    TextColor = (Color)Application.Current.Resources["AquaColor"],
                    LineBreakMode = LineBreakMode.WordWrap,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    FontSize = 12
                };
                stack.Children.Add(label);

                grid.Children.Add(stack, column, row);
                column++;
            }

            Content = grid;
        }


        async void Tap_Tapped(object sender, EventArgs e)
        {
            var stack = sender as StackLocal;
            if(stack != null)
            {
                await App.Navigator.PushAsync(new DetalleNoticiaPage(noticias.Where(x => x.id == stack.id).FirstOrDefault()));
            }
        }
    }
}
