
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
using Android.GoogleMaps;

namespace DoSomething
{
	[Activity (Label = "MapActivity")]			
	public class MapActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Map);
			// Create your application here

			//var map = new MapView (this, "AIzaSyCfMBQ_jjGA2iQu1E-XNW__aG07lVJPlhw");

		}

		protected override bool IsRouteDisplayed {
			get {
				return false;
			}
		}
	}
}

