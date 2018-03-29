using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace IDD
{
    public class ListCuotas : StackLayout
    {
        public ListCuotas(List<Cuota> cuotas)
        {
            BackgroundColor = (Color)Application.Current.Resources["GrisColor"];
            //BackgroundColor = Color.Gray;

            if(cuotas != null)
            {
                var grid = new Grid
                {
                    Margin = new Thickness(20,10),
                    RowSpacing = 10
                };
                grid.ColumnDefinitions.Add(new ColumnDefinition(){ Width = new GridLength(2)});
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(40) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                //grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                int row = 0;
                bool colorLine = false;
                foreach(var i in cuotas)
                {
                    grid.RowDefinitions.Add(new RowDefinition(){ Height = new GridLength(50) });

					var backgraound = new BoxView
					{
						BackgroundColor = Color.White
					};

					grid.Children.Add(backgraound, 0, row);
					Grid.SetColumnSpan(backgraound, 4);

                    var boxLine = new BoxView
                    {
                        BackgroundColor = (Color)Application.Current.Resources[(colorLine ? "AquaColor" : "AmarilloColor")]
                    };
                    grid.Children.Add(boxLine, 0, row);

                    var stackFecha = new StackLayout { VerticalOptions = LayoutOptions.CenterAndExpand };

                    var dia = new Label
                    {
                        Text = i.dia.ToString(),
                        TextColor = (Color)Application.Current.Resources["AzulColor"],
                        FontSize = 18,
                        FontAttributes = FontAttributes.Bold,
                        HorizontalOptions = LayoutOptions.CenterAndExpand
                    };
                    stackFecha.Children.Add(dia);

					var mes = new Label
					{
                        Text = i.mes.ToUpperInvariant(),
						TextColor = (Color)Application.Current.Resources["AzulColor"],
						FontSize = 12,
						FontAttributes = FontAttributes.Bold,
                        HorizontalOptions = LayoutOptions.CenterAndExpand
					};
                    stackFecha.Children.Add(mes);
                    grid.Children.Add(stackFecha, 1, row);

                    grid.Children.Add(new BoxView { BackgroundColor = Color.Gray, Margin = new Thickness(0 , 6), Opacity = 0.5}, 2, row);

                    var stackDetalle = new StackLayout{ VerticalOptions = LayoutOptions.CenterAndExpand, Margin = new Thickness(10,0), Orientation = StackOrientation.Horizontal };

                    var labelNombre = new Label
                    {
                        Text = i.nombre,
                        FontSize = 10,
                        TextColor = (Color)Application.Current.Resources["AzulColor"],
                        HorizontalOptions = LayoutOptions.StartAndExpand
                    };
                    stackDetalle.Children.Add(labelNombre);

                    if(i.monto > 0)
                    {
                        var labelMonto = new Label 
                        {
                            Text = i.montoString,
                            TextColor = Color.Gray,
                            FontSize = 10,
                            HorizontalOptions = LayoutOptions.EndAndExpand
                        };
                        stackDetalle.Children.Add(labelMonto);
                    }
                    grid.Children.Add(stackDetalle, 3, row);

                    row++;
                    colorLine = !colorLine;
                }
                Children.Add(grid);
            }
        }
    }
}
