using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace IDD
{
	public partial class TourPage : ContentPage
	{
		List<FestivalItem> _tour;
		public ListView ListView { get { return listTour; } }

		public TourPage()
		{
			InitializeComponent();

			ListView.ItemSelected += ListView_ItemSelected;

            _tour = FestivalItem.GetLugares();

			ListView.ItemsSource = _tour;
			ListView.BeginRefresh();
			ListView.EndRefresh();
		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();

			Title = "Lugares que visitar";
		}

		async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var item = e.SelectedItem as FestivalItem;
			ListView.SelectedItem = null;
			if (item != null)
			{
				Device.OpenUri(
					new Uri(string.Format(item.url)));
			}
		}
	}
}
