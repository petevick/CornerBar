using System;
using System.IO;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ImageCircle.Forms.Plugin.Droid;
using Xamarin;

namespace CornerBar.Droid
{
    [Activity(Theme = "@style/Theme.CornerBarApp", Label = "CornerBar", Icon = "@drawable/icon",  ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity = this;

            App.ScreenSize.Height = (int)Resources.DisplayMetrics.HeightPixels; // real pixels
            App.ScreenSize.Width = (int)Resources.DisplayMetrics.WidthPixels; // real pixels

            global::Xamarin.Forms.Forms.Init(this, bundle);
            ImageCircleRenderer.Init();

            LoadApplication(new App());

        }

        protected override void OnStop()
        {
            
            base.OnStop();


        }
        [Activity(Theme = "@style/Theme.Splash", //Indicates the theme to use for this activity
         MainLauncher = true, //Set it as boot activity
         NoHistory = true)] //Doesn't place it in back stack

        public class SplashActivity : Activity
        {
            protected override void OnCreate(Bundle bundle)
            {
                base.OnCreate(bundle);

                //RWB.App.ScreenSize.Height = (int)Resources.DisplayMetrics.HeightPixels; // real pixels
                //RWB.App.ScreenSize.Width = (int)Resources.DisplayMetrics.WidthPixels; // real pixels

                System.Threading.Thread.Sleep(100); //Let's wait awhile...
                this.StartActivity(typeof(MainActivity));
            }
        }

    }
}

