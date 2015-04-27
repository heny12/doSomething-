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
		public Button LogoutButton;
		public Button LoginButton;
		//LayoutInflater inflater;
		//View main;
		//View login;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

			//main = inflater.Inflate (Resource.Layout.Main, null);
			//login = inflater.Inflate (Resource.Layout.Login, null);
			SetContentView (Resource.Layout.Main);
			// Gets button for logout
			LogoutButton = FindViewById<Button> (Resource.Id.LogoutButton); //Goes in separate Activity
			// On click of the logout button signout the user
			LogoutButton.Click += (object sender, EventArgs a) => //Goes in separate Activity
			{
				LogoutClick ();
			};

			// Set our view from the "login" layout resource
			SetContentView (Resource.Layout.Login);
			// Gets button for login
			LoginButton = FindViewById<Button> (Resource.Id.LoginButton); //Either this becomes login activity, or we move this to different activity
			// On click of the login button authenticate the user or prompt them to login via FB
			LoginButton.Click += async (object sender, EventArgs a) => await LoginClick ();
		}
		/*
		 * TODO: Login needs to be in a separate activity than main activity.  OR, login needs to be main activity, and we go to a new activity after logging in.
		 * This will prevent the logout button from being a useless piece of shit.
		*/
		public async Task LoginClick() {
			try 
			{
				// Create the Mobile Service Client instance, using the provided
				// Mobile Service URL and key
				/*
				String url = "https://dosomethingrg12d746eb69a7445c7b98ab26c9fb63a5b.azurewebsites.net/"; Old ass url and appkey.  On .NET backend.  Doesn't allow client directed authentication without shitty hacks.
				String appKey = "zAIiBPQyGNCjAugFTPwHsoyyLeoveG53"; 
				client = new MobileServiceClient(url,appKey); */
				client = new MobileServiceClient("https://dosomething.azure-mobile.net/",
					"SIZclmUxUGubaEXCuEXKkKjDlPxBfK77");
				var success = await Authenticate();
				if (success) {
					var intent = new Intent(this, typeof(MapActivity));
					StartActivity(intent);
					//String alert = string.Format("Welcome now, you're the best, your user ID is {0}", user.UserId);
					//Toast.MakeText(this, alert, ToastLength.Long).Show();
				}

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

		private async Task<bool> Authenticate()
		{
			try
			{
				user = await client.LoginAsync(this, MobileServiceAuthenticationProvider.Facebook);
				//String alert = string.Format("Welcome Henry, you're the best, your user ID is {0}", user.UserId);
				String alert = "Welcome to Do Something!";
				Toast.MakeText(this, alert, ToastLength.Long).Show();
				return true;

				//SetContentView (Resource.Layout.Main);
			}
			catch (Exception ex)
			{
				Toast.MakeText(this, "Authentication failed", ToastLength.Long).Show();
				return false;
			}
		}

		/*
		 *TODO: All of the logout Activity stuff needs to be moved to the post-login screen (tabbed view, map, etc)
		 *All this code gets its shit overwritten when we change layouts, so we need to move this to a different activity.
		*/
		public async void LogoutClick() {

			try 
			{
				await Logout();
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


