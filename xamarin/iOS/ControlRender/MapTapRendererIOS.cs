using System;
using IDD;
using IDD.iOS;
using MapKit;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MapTapRenderer), typeof(MapTapRendererIOS))]
namespace IDD.iOS
{
    public class MapTapRendererIOS : MapRenderer
    {
		readonly UITapGestureRecognizer _tapRecogniser;

		IGeolocator locationG;
		Position position;

		public MapTapRendererIOS()
		{
			_tapRecogniser = new UITapGestureRecognizer(OnTap)
			{
				NumberOfTapsRequired = 1,
				NumberOfTouchesRequired = 1
			};
		}

		async void OnTap(UITapGestureRecognizer recognizer)
		{
			var cgPoint = recognizer.LocationInView(Control);
			var location = ((MKMapView)Control).ConvertPoint(cgPoint, Control);
			await ((MapTapRenderer)Element).ChangePosition(new Xamarin.Forms.Maps.Position(location.Latitude, location.Longitude));
		}

		protected async override void OnElementChanged(ElementChangedEventArgs<View> e)
		{
			if ((MapTapRenderer)Element != null)
			{
				if (Control != null)
					Control.RemoveGestureRecognizer(_tapRecogniser);
				base.OnElementChanged(e);
				if (Control != null && ((MapTapRenderer)Element).IsEnabled)
					Control.AddGestureRecognizer(_tapRecogniser);

				locationG = CrossGeolocator.Current;
				locationG.DesiredAccuracy = 100;

				try
				{
					position = await locationG.GetPositionAsync(TimeSpan.FromSeconds(10));

					await ((MapTapRenderer)Element).ChangePosition(new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude));

					((MapTapRenderer)Element).SetLocation(new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude));
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}
		}

        public void GetCurrent()
        {
            
        }
    }
}
