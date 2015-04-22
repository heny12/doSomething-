using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

namespace DoSomething
{
	[Activity (Label = "DoSomething", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		private MobileServiceUser user;
		private MobileServiceClient client;

		protected override async void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			await Authenticate();
			

		}
			
		private async Task Authenticate()
		{
			try
			{
				user = await client.LoginAsync(this, MobileServiceAuthenticationProvider.Facebook);
				String alert = string.Format("you are now logged in - {0}", user.UserId);
				Toast.MakeText(this, alert, ToastLength.Long);
			}
			catch (Exception ex)
			{
				Toast.MakeText(this, "Authentication failed", ToastLength.Long);
			}
		}
	}
}


