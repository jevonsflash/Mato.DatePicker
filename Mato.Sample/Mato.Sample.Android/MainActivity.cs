using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Mato.DatePicker.Android;

namespace Mato.Sample.Droid
{
    [Activity(Label = "Mato.Sample", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            AndroidEnvironment.UnhandledExceptionRaiser += UnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            ChinaDateServer ssChinaDateServer = new ChinaDateServer();

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        private void UnhandledException(object sender, RaiseThrowableEventArgs e)
        {
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
        }
    }
}

