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

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "login" layout resource
			SetContentView (Resource.Layout.Login);

			// Gets button for logout
			Button LogoutButton = FindViewById<Button> (Resource.Id.LogoutButton);

			// Gets button for login
			Button LoginButton = FindViewById<Button> (Resource.Id.LoginButton);

			// On click of the logout button signout the user
			LogoutButton.Click += (object sender, EventArgs a) =>
			{

				try 
				{
					Logout();
				}
				catch (Java.Net.MalformedURLException) 
				{
					Toast.MakeText(this, "URL Error", ToastLength.Long).Show();
				} 
				catch (Exception e) 
				{
					Toast.MakeText(this, e.Message, ToastLength.Long).Show();
				}

			};

			// On click of the login button authenticate the user or prompt them to login via FB
			LoginButton.Click += async (object sender, EventArgs a) =>
			{

				try 
				{
					// Create the Mobile Service Client instance, using the provided
					// Mobile Service URL and key
					String url = "https://dosomethingrg12d746eb69a7445c7b98ab26c9fb63a5b.azurewebsites.net/";
					String appKey = "zAIiBPQyGNCjAugFTPwHsoyyLeoveG53"; 
					//client = new MobileServiceClient(
					//Resource.String.appURL.ToString(),
					//Resource.String.AzureKey.ToString());
					client = new MobileServiceClient(url,appKey);
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

			};

		}

		private async Task Authenticate()
		{
			try
			{
				user = await client.LoginAsync(this, MobileServiceAuthenticationProvider.Facebook);
				String alert = string.Format("Welcome Henry, you're the best, your user ID is {0}", user.UserId);
				Toast.MakeText(this, alert, ToastLength.Long).Show();
				SetContentView (Resource.Layout.Main);
			}
			catch (Exception ex)
			{
				Toast.MakeText(this, "Authentication failed", ToastLength.Long).Show();
			}
		}
		private async Task Logout()
		{
			try
			{
				client.Logout ();
				String alert = string.Format("Successfully logged out");
				Toast.MakeText(this, alert, ToastLength.Long).Show();
				SetContentView (Resource.Layout.Login);
			}
			catch (Exception ex)
			{
				Toast.MakeText(this, "Logout failed", ToastLength.Long).Show();
			}
		}
	}
}


