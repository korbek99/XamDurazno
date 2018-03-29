using IDD;
using IDD.Droid;

using System;
using Android.Gms.Maps;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;
using Android.Gms.Maps.Model;

[assembly: ExportRenderer(typeof(MapTapRenderer), typeof(MapTapRendererDroid))]
namespace IDD.Droid
{
    public class MapTapRendererDroid : MapRenderer, IOnMapReadyCallback
    {
		GoogleMap _map;
		//Android.Locations.Geocoder geocoder;
		IGeolocator location;
		Plugin.Geolocator.Abstractions.Position position;

		public void OnMapReady(GoogleMap googleMap)
		{
			_map = googleMap;
			if (_map != null && ((MapTapRenderer)Element).IsEnabled)
				_map.MapClick += googleMap_MapClick;

			if (((MapTapRenderer)Element).IsEnabled)
			{
				_map.UiSettings.ZoomControlsEnabled = true;
				_map.UiSettings.MyLocationButtonEnabled = true;
				_map.UiSettings.RotateGesturesEnabled = false;
			}
			else
			{
				_map.UiSettings.TiltGesturesEnabled = false;
				_map.UiSettings.ScrollGesturesEnabled = false;
			}

            ((MapTapRenderer)Element).GetCurrentEvent += Handle_GetCurrentEvent1;

   //         _map.MaxZoomLevel(20);
			//_map.SetMinZoomPreference(5);
		}

		protected async override void OnElementChanged(ElementChangedEventArgs<Map> e)
		{
			if (_map != null)
				_map.MapClick -= googleMap_MapClick;
			base.OnElementChanged(e);
			if (Control != null)
				((MapView)Control).GetMapAsync(this);

			location = CrossGeolocator.Current;

			location.DesiredAccuracy = 100;

			try
			{
				position = await location.GetPositionAsync(TimeSpan.FromSeconds(10));

				await ((MapTapRenderer)Element).ChangePosition(new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude));


				Console.WriteLine("pos android" + position.Latitude.ToString() + " , " + position.Longitude.ToString());
				SetMarker(position.Latitude, position.Longitude);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		async void googleMap_MapClick(object sender, GoogleMap.MapClickEventArgs e)
		{
			//((MapTapRenderer)Element).OnTap(new Xamarin.Forms.Maps.Position(e.Point.Latitude, e.Point.Longitude));

			await ((MapTapRenderer)Element).ChangePosition(new Xamarin.Forms.Maps.Position(e.Point.Latitude, e.Point.Longitude));
			SetMarker(e.Point.Latitude, e.Point.Longitude);
		}

		void SetMarker(double lat, double lon)
		{
			_map.Clear();
			_map.AddMarker(new Android.Gms.Maps.Model.MarkerOptions().SetPosition(new Android.Gms.Maps.Model.LatLng(lat, lon)).SetTitle("Denuncia"));
			_map.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(lat, lon), 15));
		}

        #region CODIGO PARA LISTENER DE ACTUALIZACIÓN DE LOCALIZACIÓN
        //await CrossGeolocator.Current.StartListeningAsync(5, 10, true);

        //CrossGeolocator.Current.PositionChanged += Current_PositionChanged;

        //void Current_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        //{
        //  Device.BeginInvokeOnMainThread(() =>
        //  {
        //      var test = e.Position;
        //      Toast.MakeText(Android.App.Application.Context, "te" + test.Latitude, ToastLength.Long).Show();
        //      ((MapTapRenderer)Element).ChangePosition(new Xamarin.Forms.Maps.Position(e.Position.Latitude, e.Position.Longitude));
        //  });
        //}
        #endregion

        async void Handle_GetCurrentEvent1(object sender, EventArgs e)
        {
			location = CrossGeolocator.Current;

			location.DesiredAccuracy = 100;

			try
			{
				position = await location.GetPositionAsync(TimeSpan.FromSeconds(10));

				await((MapTapRenderer)Element).ChangePosition(new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude));

				SetMarker(position.Latitude, position.Longitude);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
        }
    }
}
