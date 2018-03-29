using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace IDD
{
    public class MapTapRenderer : Map
    {
		public event EventHandler<TapEventArgs> Tap;
        public event EventHandler<EventArgs> GetCurrentEvent;


        IEnumerable<string> possibleAddresses;
		//public DireccionViewModel direccionViewModel;
		Geocoder geoCoder;
        //public CrearDenunciaPage denunciaPAge;
        public Xamarin.Forms.Maps.Position coordinate;
#if __IOS__
		Pin pin;
#endif

        IGeolocator locationG;
        Plugin.Geolocator.Abstractions.Position position;

		public MapTapRenderer()
		{
			//direccionViewModel = new DireccionViewModel();
			geoCoder = new Geocoder();
		}

		public MapTapRenderer(MapSpan region) : base(region)
		{
			//direccionViewModel = new DireccionViewModel();
			geoCoder = new Geocoder();
		}

		public async Task ChangePosition(Xamarin.Forms.Maps.Position coordinate)
		{
            this.coordinate = coordinate;
			possibleAddresses = await geoCoder.GetAddressesForPositionAsync(coordinate);
			if (possibleAddresses != null)
			{
				//direccionViewModel.LoadDireccion(possibleAddresses.FirstOrDefault()
				//								 , coordinate.Latitude, coordinate.Longitude);
				SetLocation(coordinate);
			}

		}

		//public void OnTap(Position coordinate)
		//{
		//	OnTap(new TapEventArgs { Position = coordinate });
		//}

		public async Task GetCurrent()
		{
			locationG = CrossGeolocator.Current;
			locationG.DesiredAccuracy = 100;

			try
			{
				position = await locationG.GetPositionAsync(TimeSpan.FromSeconds(10));

				await ChangePosition(new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude));

				SetLocation(new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude));
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		protected virtual void OnTap(TapEventArgs e)
		{
			var handler = Tap;
			if (handler != null)
			{
				handler(this, e);
			}
		}

        public virtual void OnGetCurrentEvent(object sender, EventArgs e)
		{
            var handler = GetCurrentEvent;
			if (handler != null)
			{
                handler(sender, e);
			}
		}

		public void SetLocation(Xamarin.Forms.Maps.Position coordinate)
		{
			var pos = new Xamarin.Forms.Maps.Position(coordinate.Latitude, coordinate.Longitude);
			MoveToRegion(MapSpan.FromCenterAndRadius(pos, Distance.FromMiles(0.1)));
#if __IOS__
			if (pin != null)
				Pins.Remove(pin);
            pin = new Pin { Position = coordinate, Address = possibleAddresses.FirstOrDefault().Split('\n')[0], Label = "Reclamo" };
			Pins.Add(pin);
#endif
			//if (denunciaPAge != null) denunciaPAge.Loading(false);
		}
	}
	public class TapEventArgs : EventArgs
	{
		public Xamarin.Forms.Maps.Position Position { get; set; }
	}

}
