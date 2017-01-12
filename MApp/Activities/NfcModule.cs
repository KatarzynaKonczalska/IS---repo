using System;
using System.Text;

using Android.App;
using Android.Content;
using Android.Nfc;
using Android.Nfc.Tech;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;

using Java.IO;

namespace MApp.Activities
{
    [Activity(Label = "nfc", Icon = "@drawable/icon")]
    public class NfcModule : Activity
    {

        public const string ViewApeMimeType = "application/vnd.xamarin.nfcxample";
        public static readonly string NfcAppRecord = "xamarin.nfxample";
        public static readonly string Tag = "Impero wykry³ TAG  NFC";

        private bool _inWriteMode;
        private NfcAdapter _nfcAdapter;
        private TextView _textView;
        private Button _writeTagButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Nfc);

            _nfcAdapter = NfcAdapter.GetDefaultAdapter(this);
            _textView = FindViewById<TextView>(Resource.Id.text_view);
        }

        private void TagDetecting()
        {
            _inWriteMode = true;

            // Create an intent filter for when an NFC tag is discovered.  When
            // the NFC tag is discovered, Android will u
            var tagDetected = new IntentFilter(NfcAdapter.ActionTagDiscovered);
            var filters = new[] { tagDetected };

            // When an NFC tag is detected, Android will use the PendingIntent to come back to this activity.
            // The OnNewIntent method will invoked by Android.
            var intent = new Intent(this, GetType()).AddFlags(ActivityFlags.SingleTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, 0);

            if (_nfcAdapter == null)
            {
                var alert = new AlertDialog.Builder(this).Create();
                alert.SetMessage("NFC is not supported on this device.");
                alert.SetTitle("NFC Unavailable");
                alert.SetButton("OK", delegate {
                    _writeTagButton.Enabled = false;
                    _textView.Text = "NFC is not supported on this device.";
                });
                alert.Show();
            }
            else
                _nfcAdapter.EnableForegroundDispatch(this, pendingIntent, filters, null);
        }

    }
}