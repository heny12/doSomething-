
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Locations;
using Android.Gms.Location;
using Android.Gms.Common;
using Geolocator.Plugin;


namespace DoSomething
{
	[Activity (Label = "MapActivity")]			
	public class MapActivity : Activity, IOnMapReadyCallback, Android.Gms.Location.ILocationListener
	{
		Location currentLocation;
		LocationManager manager;
		string provider;
		GoogleMap map;

		public void OnLocationChanged(Location location)
		{
			currentLocation = location;
			if (currentLocation == null)
			{
				Toast.MakeText(this, "Location Error", ToastLength.Long).Show();
			}
			else
			{
				var newLocation = new LatLng(location.Latitude, location.Longitude);
				//map.MoveCamera (CameraUpdateFactory.NewLatLngZoom(newLocation, 10));
				map.MoveCamera(CameraUpdateFactory.NewLatLng(newLocation));
			}
		}

		public void OnProviderDisabled(string provider)
		{
		}

		public void OnProviderEnabled(string provider)
		{
		}

		public void OnStatusChanged(string provider, Availability status, Bundle extras)
		{
		}

		protected override async void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Map);
			makeLocationManager();
			//var locator = CrossGeolocator.Current;
			//locator.DesiredAccuracy = 50;
			//var position = await locator.GetPositionAsync (timeout: 10000);
			var mapFragment = new MapFragment ();
			FragmentTransaction fragmentTx = this.FragmentManager.BeginTransaction();
			fragmentTx.Add (Resource.Id.linearLayout1, mapFragment);
			fragmentTx.Commit ();
			map = mapFragment.Map;
			/*
			if (map != null) {
				var coordinates = new LatLng (position.Latitude, position.Longitude);
				map.MoveCamera (CameraUpdateFactory.NewLatLng (coordinates));
			} */


			// Create your application here


		}

		void makeLocationManager()
		{
			manager = (LocationManager)GetSystemService(LocationService);
			Criteria criteriaForLocationService = new Criteria
			{
				Accuracy = Accuracy.Fine
			};
			IList<string> acceptableLocationProviders = manager.GetProviders(criteriaForLocationService, true);

			if (acceptableLocationProviders.Any())
			{
				provider = acceptableLocationProviders.First();
			}
		}
			

		public async void OnMapReady (GoogleMap map)
		{
			this.map = map;
			//Setup and customize your Google Map
			this.map.UiSettings.CompassEnabled = false;
			this.map.UiSettings.MyLocationButtonEnabled = false;
			this.map.UiSettings.MapToolbarEnabled = false;
		}
	}
}

