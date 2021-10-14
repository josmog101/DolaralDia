using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Dolaraldia
{
    public static class Constants
    {
        public static string AdUnitIdTest
        {
            get
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.Android:
                        return "ca-app-pub-3155576757043096/1540970003";
                        //return "ca-app-pub-3940256099942544/6300978111";

                    /* case Device.iOS:
                         return "ca-app-pub-3940256099942544/2934735716";*/

                    default:
                        throw new InvalidOperationException("Invalid platform");
                }
            }
        }
      /*  public const string ListenConnectionString = "<Listen connection string>";
        public const string NotificationHubName = "<hub name>";*/
    }
}
