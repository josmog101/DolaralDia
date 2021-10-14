using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Dolaraldia.CustomControls;
using Dolaraldia.Droid;
using Dolaraldia.ViewModels;
using Firebase;
using Firebase.Messaging;
using Plugin.LocalNotification;
using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Dolaraldia.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        public override void OnMessageReceived(RemoteMessage message)
        {
            base.OnMessageReceived(message);
            FirebaseApp.InitializeApp(this);
            var titulo = message.Data["title"];
            ShowNotification(titulo);
       
        }
        private void ShowNotification(string titulo)
        {
            LocalNotificationsImplementation.NotificationIconId = Resource.Drawable.notification;
            Plugin.LocalNotifications.CrossLocalNotifications.Current.Show(null,titulo,0);
        }

        /*private void SendNotification(string title)
        {
            var intent = new Intent(this, typeof(MainActivity));
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);
            var defaultSoundUri = RingtoneManager.GetDefaultUri(RingtoneType.Notification);

            Notification.BigTextStyle textStyle = new Notification.BigTextStyle();
            textStyle.BigText(title);
            textStyle.SetSummaryText("Ver mas...");

            var notificationBuilder = new Notification.Builder(this)
                .SetSmallIcon(Resource.Drawable.notification)
                .SetContentTitle("Dólar al Día")
                .SetStyle(textStyle)
                //.SetContentText(title)
                .SetContentText("prueba")
                .SetContentText("prueba2")
                .SetSound(defaultSoundUri)
                .SetVibrate(new long[] { 0, 1000, 0, 0, 0 })
                .SetAutoCancel(true)
                .SetLights(0x00FF00, 300, 100)
                .SetContentIntent(pendingIntent);

            var notificationManager = NotificationManager.FromContext(this);
            notificationManager.Notify(0, notificationBuilder.Build());
        }
        */
    }
}