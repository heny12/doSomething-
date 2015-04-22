using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

namespace DoSomething.Droid
{
	[Activity (Label = "DoSomething.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		private MobileServiceUser user;
		private MobileServiceClient client;

		protected override async void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);

			LoadApplication (new App ());
			await Authenticate();
		}

		private async Task Authenticate()
		{
			try
			{
				user = await client.LoginAsync(this, MobileServiceAuthenticationProvider.MicrosoftAccount);
				CreateAndShowDialog(string.Format("you are now logged in - {0}", user.UserId), "Logged in!");
			}
			catch (Exception ex)
			{
				CreateAndShowDialog(ex, "Authentication failed");
			}
		}
	}
}

