using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Service;


namespace MApp.Services
{
    [Service(Label = "NfcService")]
    public class NfcService : Service
    {
        public override StartCommandResult OnStartCommand(Android.Content.Intent intent, StartCommandFlags flags, int startId)
        {
            //Log.Debug("DemoService", "DemoService started");

            return StartCommandResult.Sticky;
        }

        // zatrzymuje servis po zakoñczeniu zadania
        public void DoWork()
        {
            var t = new Thread(() => {
                //akcja
                Thread.Sleep(5000);
                //akcja
                StopSelf();
            }
            );
            t.Start();
        }

        public override IBinder OnBind(Intent intent)
        {
            // It should simply return null for a started service that is not also a bound service.
            return null;
        }

    }
} 