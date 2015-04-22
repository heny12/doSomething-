using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using System.Net.Http;


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
			try 
			{
				// Create the Mobile Service Client instance, using the provided
				// Mobile Service URL and key
				String url = "https://dosomethingrg12d746eb69a7445c7b98ab26c9fb63a5b.azurewebsites.net/";
				String appKey = "zAIiBPQyGNCjAugFTPwHsoyyLeoveG53"; 
				//client = new MobileServiceClient(
					//Resource.String.appURL.ToString(),
					//Resource.String.AzureKey.ToString());
				client = new MobileServiceClient(
					url,
					appKey);

				await Authenticate();
			}
            catch (Java.Net.MalformedURLException) 
            {
				Toast.MakeText(this, "URL Error", ToastLength.Long).Show();
			} 
            catch (Exception e) 
            {
				Toast.MakeText(this, e.Message, ToastLength.Long).Show();
			}
		}
			
		private async Task Authenticate()
		{
			try
			{
				user = await client.LoginAsync(this, MobileServiceAuthenticationProvider.Facebook);
				String alert = string.Format("you are now logged in - {0}", user.UserId);
				Toast.MakeText(this, alert, ToastLength.Long).Show();
			}
			catch (Exception ex)
			{
				Toast.MakeText(this, "Authentication failed", ToastLength.Long).Show();
			}
		}
	}
}


