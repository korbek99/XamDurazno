using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace IDD
{
    public class Accordion : ContentView
    {
        #region Private Variables
        List<AccordionSource> mDataSource;
        int idVisible;
        StackLayout mMainLayout;
        TapGestureRecognizer tapHead;
        #endregion

        public Accordion()
        {
            mMainLayout = new StackLayout();
            Content = mMainLayout;

            tapHead = new TapGestureRecognizer();
            tapHead.Tapped += TapHead_Tapped;
        }

        public Accordion(List<AccordionSource> aSource)
        {
            mDataSource = aSource;
            DataBind();
        }

        #region Properties
        public List<AccordionSource> DataSource
        {
            get { return mDataSource; }
            set { mDataSource = value; }
        }
        #endregion

        public void DataBind()
        {
            var vMainLayout = new StackLayout();
            if (mDataSource != null)
            {
                foreach (var vSingleItem in mDataSource)
                {

                    var vHeaderButton = new AccordionGrid();
                    vHeaderButton.PreparateGrid(vSingleItem.rubro);

                    var vAccordionContent = new ContentView()
                    {
                        Content = vSingleItem.ContentItems,
                        IsVisible = false,
                        Margin = new Thickness(0, -6, 0, 0)
                    };
                    vHeaderButton.AssosiatedContent = vAccordionContent;
                    vHeaderButton.GestureRecognizers.Add(tapHead);
                    vMainLayout.Children.Add(vHeaderButton);
                    vMainLayout.Children.Add(vAccordionContent);

                }
            }
            mMainLayout = vMainLayout;
            Content = mMainLayout;
        }

        void TapHead_Tapped(object sender, EventArgs e)
        {
            var vGrid = (AccordionGrid)sender;
            if (vGrid.id == idVisible)
            {
                vGrid.Expand = !vGrid.Expand;
                vGrid.imgFlecha.Source = vGrid.Expand ? "calendario_flecha_arriba" : "calendario_flecha_abajo";
                vGrid.AssosiatedContent.IsVisible = vGrid.Expand;
            }
            else
            {
                foreach (var vChildItem in mMainLayout.Children)
                {
                    if (vChildItem.GetType() == typeof(ContentView)) vChildItem.IsVisible = false;
                    if (vChildItem.GetType() == typeof(AccordionGrid))
                    {
                        var vButton = (AccordionGrid)vChildItem;
                        vButton.Expand = false;
                        vButton.imgFlecha.Source = "calendario_flecha_abajo";
                    }
                }

                vGrid.Expand = true;
                vGrid.imgFlecha.Source = "calendario_flecha_arriba";
                vGrid.AssosiatedContent.IsVisible = vGrid.Expand;
                idVisible = vGrid.id;

			}
        }
    }

    public class AccordionGrid : Grid
    {
        #region Private Variables
        bool mExpand = false;
        #endregion
        public AccordionGrid()
        {
            HorizontalOptions = LayoutOptions.FillAndExpand;
            HeightRequest = 60;
            ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(45) });
            ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(30) });
            RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1) });
        }
        #region Properties
        public int id;
        public bool Expand
        {
            get { return mExpand; }
            set { mExpand = value; }
        }
        public ContentView AssosiatedContent
        { get; set; }
        public Image imgFlecha;
        #endregion

        public void PreparateGrid(Rubro rubro)
        {
            if (rubro != null)
            {
                id = rubro.id;

                var imgIcon = new Image
                {
                    Margin = new Thickness(10, 0, 0, 0),
                    Aspect = Aspect.AspectFit,
                    Source = rubro.icono,
                };
                Children.Add(imgIcon, 0, 0);

                imgFlecha = new Image
                {
                    Margin = new Thickness(0, 0, 10, 0),
                    Aspect = Aspect.AspectFit,
                    Source = "calendario_flecha_abajo",
                };
                Children.Add(imgFlecha, 2, 0);

                var stack = new StackLayout { VerticalOptions = LayoutOptions.CenterAndExpand };
                var labelNombre = new Label
                {
                    Text = rubro.nombre,
                    FontSize = 13,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = (Color)Application.Current.Resources["AquaColor"]
                };
                stack.Children.Add(labelNombre);

                var labelCuota = new Label
                {
                    Text = rubro.year,
                    FontSize = 10,
                    TextColor = (Color)Application.Current.Resources["AzulColor"]
                };
                stack.Children.Add(labelCuota);
                Children.Add(stack, 1, 0);

                var boxLine = new BoxView
                {
                    BackgroundColor = Color.Gray,
                    Opacity = 0.8,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };

                Children.Add(boxLine, 0, 1);
                SetColumnSpan(boxLine, 3);
            }
        }
    }

    public class AccordionSource
    {
        public Rubro rubro;
        public View ContentItems
        { get; set; }
    }
}

