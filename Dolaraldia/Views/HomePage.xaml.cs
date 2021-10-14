using Dolaraldia.Helpers;
using Dolaraldia.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Dolaraldia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
           /* AdModAds = new CustomControls.AdControlView
            {
                AdUnitId = Constants.AdUnitIdTest
            };*/
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            // MainViewModel.GetInstace().CommentInsta.ChangeCheck();

            MessagingCenter.Subscribe<MainViewModel, TransitionType>(this, AppSettings.TransitionMessage, (sender, arg) =>
            {
                var transitionType = (TransitionType)arg;
                var transitionNavigationPage = Parent as CustomControls.TransitionNavigationPage;

                if (transitionNavigationPage != null)
                {
                    transitionNavigationPage.TransitionType = transitionType;
                   /* switch (sender.typeView)
                    {
                        case "AboutPage":
                            Navigation.PushAsync(new AboutPage());
                            break;                        
                    }*/
                }
            });
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<MainViewModel, TransitionType>(this, AppSettings.TransitionMessage);
        }

        private void StandardEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (EntryDolar.IsFocused == true)
            {
                MainViewModel.GetInstace().Home.ConverToBs();
            }
        }

        private void StandardEntry_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (EntryBs.IsFocused == true)
            {
                MainViewModel.GetInstace().Home.ConverToDolar();
            }
        }

        private void StandardEntry_TextChanged_2(object sender, TextChangedEventArgs e)
        {
            if (EntryBsD.IsFocused == true)
            {
                MainViewModel.GetInstace().Home.ConverToDigital();
            }
        }
    }
}