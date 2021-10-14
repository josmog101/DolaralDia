using Dolaraldia.Helpers;
using Dolaraldia.Models;
using GalaSoft.MvvmLight.Command;
using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Dolaraldia.ViewModels
{
    public class MainViewModel
    {
        private static MainViewModel instance;
        public string typeView = "";
        public HomeViewModel Home
        {
            get;
            set;
        }
        public ApiData DataApi
        {
            get;
            set;
        }
        public MainViewModel()
        {
            instance = this;
           // Home = new HomeViewModel();
        }
        public static MainViewModel GetInstace()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }
        public void MessageTransition(string valor)
        {
            typeView = valor;
            MessagingCenter.Send(this, AppSettings.TransitionMessage, TransitionType.SlideFromRight);
        }
        public ICommand RefreshToolbarCommand
        {
            get
            {
                return new RelayCommand(RefreshToolbar);
            }

        }

        private void RefreshToolbar()
        {
            Home.LoadData();
        }
        public ICommand ShareFastToolbarCommand
        {
            get
            {
                return new RelayCommand(ShareFastToolbar);
            }

        }

        private async void ShareFastToolbar()
        {
            if (Screenshot.IsCaptureSupported)
            {
                var _screenshot = await Screenshot.CaptureAsync();
                var _stream = await _screenshot.OpenReadAsync();
                var _imgScreenshot = ImageSource.FromStream(()=> _stream);

                var _imageArray = FilesHelper.ReadFully(_stream);
                SaveBytes("screenshotdh.jpg", _imageArray);
                var fn = "Attachment.jpg";
                var file = Path.Combine(FileSystem.CacheDirectory, fn);
                File.WriteAllBytes(file, _imageArray);
                


                 await Share.RequestAsync(new ShareFileRequest
                 {
                     File = new ShareFile(Path.Combine(FileSystem.CacheDirectory, file))
                 });
            }
        }
        public static void SaveBytes(string fileName, byte[] data)
        {
            var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);
            if (!File.Exists(filePath))
                File.Delete(filePath);
            File.WriteAllBytes(filePath, data);
        }
        public ICommand MyShareToolbarCommand
        {
            get
            {
                return new RelayCommand(MyShare);
            }

        }
        private async void MyShare()
        {
            string _displayOpc;
            _displayOpc = await Application.Current.MainPage.DisplayActionSheet("", "", "",
                   "Compartir app",
                   "Calificar app",
                   "Acerca de");
            switch (_displayOpc)
            {
                case "Compartir app":
                    {
                        await Share.RequestAsync(new ShareTextRequest
                        {
                            Uri = Application.Current.Resources["UrlGooglePlay"].ToString(),
                            Title = "Dólar al Día"
                        });
                        break;
                    }
                case "Calificar app":
                    {
                        //Device.OpenUri(new Uri(Application.Current.Resources["UrlApp"].ToString()));
                        await Browser.OpenAsync("https://play.google.com/store", BrowserLaunchMode.SystemPreferred);
                        break;
                    }
                case "Acerca de":
                    {
                        //MainViewModel.GetInstace().AboutView = new AboutViewModel();
                        MessageTransition("AboutPage");
                        //await App.Navigator.PushAsync(new AcercaDePage());
                        break;
                    }
            }
        }
    }
}
