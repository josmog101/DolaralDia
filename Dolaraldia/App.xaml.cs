using Dolaraldia.CustomControls;
using Dolaraldia.ViewModels;
using Dolaraldia.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Dolaraldia
{
    public partial class App : Application
    {
        public static TransitionNavigationPage Navigator { get; internal set; }
        
        public App()
        {
            InitializeComponent();
            ExperimentalFeatures.Enable(ExperimentalFeatures.ShareFileRequest);
            //MainPage = new MainPage();
            MainViewModel.GetInstace().Home = new HomeViewModel();
            MainPage = new TransitionNavigationPage(new HomeNewPage());
            //CrossFirebasePushNotification.Current.OnTokenRefresh += Current_OnTokenRefresh;
            // Token event
           /* CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");
            };*/
            // Push message received event
            /*CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {

                System.Diagnostics.Debug.WriteLine("Received");

            };
            //Push message received event
            CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Opened");
                foreach (var data in p.Data)
                {
                    System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                }

            };*/
        }

        /*private void Current_OnTokenRefresh(object source, FirebasePushNotificationTokenEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"TOKEN : {e.Token}");
        }*/

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
