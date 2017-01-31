using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Nfc;

namespace MApp.Nfc
{
    public static class ImperoNfcAdapter
    {

        public static void SaveOnTag(Activity a)
        {
            var tagDetected = new IntentFilter(NfcAdapter.ActionTagDiscovered);
            a.Intent = tagDetected;
            
        }

        public static void ReadFromTag()
        {

        }

        public static void ClearTag()
        {

        }

    }
}