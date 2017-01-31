using Android.App;
using Android.OS;
using Android.Views;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V4.View;
using Android.Nfc;
using Android.Widget;
using Android.Content;
using System;
using Android.Renderscripts;
using Android;
using Java.IO;
using Android.Nfc.Tech;
using Android.Util;
using System.Text;
using MApp.Fragments;
using MApp.REST;

namespace MApp.Activities
{
    [Activity(Label = "Content", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    //[IntentFilter(new[] { "android.nfc.action.NDEF_DISCOVERED" },
    //    DataMimeType = ViewApeMimeType,
    //    Categories = new[] { "android.intent.category.DEFAULT" })]
    public class Content : AppCompatActivity, CheckOutInterface, CheckInInterface
    {
        #region Fields
        DrawerLayout drawerLayout;
        int backCount = 0;
        #endregion

        #region REST
        // TODO: Adres servera
        RESTconnection REST = new RESTconnection("http://158.75.44.109:8000");
        #endregion

        #region NFC Fields
        public static string id, id2;
        public static bool write = false;
        public static bool _tagWritten;
        public const string ViewApeMimeType = "application/vnd.xamarin.nfcxample";
        public static readonly string NfcAppRecord = "xamarin.nfxample";
        public static readonly string Tag = "NfcXample";
        public static bool _inWriteMode = false;
        public static bool _inClearMode = false;
        public static bool Clear = false;
        public static NfcAdapter _nfcAdapter;
        string hominidName;
        #endregion

        #region Override
        protected override void OnCreate(Bundle savedInstanceState)
        {
            _nfcAdapter = NfcAdapter.GetDefaultAdapter(this);
            RequestWindowFeature(WindowFeatures.NoTitle);

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            // Init toolbar
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.app_bar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetTitle(Resource.String.app_name);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);

            // Attach item selected handler to navigation view
            var navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;

            // Create ActionBarDrawerToggle button and add it to the toolbar
            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.open_drawer, Resource.String.close_drawer);
            drawerLayout.SetDrawerListener(drawerToggle);
            drawerToggle.SyncState();

            //load default home screen
            var ft = FragmentManager.BeginTransaction();
            var QM = new Fragments.QuickMenu();
            ft.Add(Resource.Id.HomeFrameLayout, QM, "quickmenu");
            ft.AddToBackStack(null);
            ft.Commit();

            handlingIntent(this.Intent);
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            handlingIntent(intent);
        }

        private void handlingIntent(Intent intent)
        {
            if (_inWriteMode)
            {
                _inWriteMode = false;
                var tag = intent.GetParcelableExtra(NfcAdapter.ExtraTag) as Tag;

                if (tag == null)
                {
                    return;
                }

                if (_inClearMode == true)
                {
                    var payload = Encoding.ASCII.GetBytes("");
                    var mimeBytes = Encoding.ASCII.GetBytes(ViewApeMimeType);
                    var apeRecord = new NdefRecord(NdefRecord.TnfMimeMedia, mimeBytes, new byte[0], payload);
                    var ndefMessage = new NdefMessage(new[] { apeRecord });

                    if (!TryAndWriteToTag(tag, ndefMessage))
                    {
                        TryAndFormatTagWithMessage(tag, ndefMessage);
                    }
                }
                else
                {
                    var payload = Encoding.ASCII.GetBytes("" + id2.ToString());
                    var mimeBytes = Encoding.ASCII.GetBytes(ViewApeMimeType);
                    var apeRecord = new NdefRecord(NdefRecord.TnfMimeMedia, mimeBytes, new byte[0], payload);
                    var ndefMessage = new NdefMessage(new[] { apeRecord });

                    if (!TryAndWriteToTag(tag, ndefMessage))
                    {
                        TryAndFormatTagWithMessage(tag, ndefMessage);
                    }
                }

            }
            else
            {
                var intentType = intent.Type;
                //Toast.MakeText(this, intentType, ToastLength.Long).Show();
                //czytanie
                if (ViewApeMimeType.Equals(intentType))
                {
                    //Toast.MakeText(this, "czyta", ToastLength.Short).Show();
                    var rawMessages = intent.GetParcelableArrayExtra(NfcAdapter.ExtraNdefMessages);
                    var msg = (NdefMessage)rawMessages[0];
                    var hominidRecord = msg.GetRecords()[0];
                    hominidName = Encoding.ASCII.GetString(hominidRecord.GetPayload());
                    id = hominidName;
                }
            }
            if(Clear)
            {
                var tag = intent.GetParcelableExtra(NfcAdapter.ExtraTag) as Tag;

                if (tag == null)
                {
                    return;
                }

                var payload = Encoding.ASCII.GetBytes("");
                var mimeBytes = Encoding.ASCII.GetBytes(ViewApeMimeType);
                var apeRecord = new NdefRecord(NdefRecord.TnfMimeMedia, mimeBytes, new byte[0], payload);
                var ndefMessage = new NdefMessage(new[] { apeRecord });

                if (!TryAndWriteToTag(tag, ndefMessage))
                {
                    TryAndFormatTagWithMessage(tag, ndefMessage);
                }
            }
            Clear = false;
            //zapisywanie
        }

        public void EnableWriteMode()
        {
            _inWriteMode = true;
            var tagDetected = new IntentFilter(NfcAdapter.ActionTagDiscovered);
            var filters = new[] { tagDetected };

            var intent = new Intent(this, GetType()).AddFlags(ActivityFlags.SingleTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, 0);

            if (_nfcAdapter == null)
            {
                var alert = new Android.App.AlertDialog.Builder(this).Create();
                alert.SetMessage("NFC is not supported on this device.");
                alert.SetTitle("NFC Unavailable");
                alert.SetButton("OK", delegate
                {

                    Toast.MakeText(this, "NFC is not supported on this device", ToastLength.Short);
                });
                alert.Show();
            }
            else
                _nfcAdapter.EnableForegroundDispatch(this, pendingIntent, filters, null);
        }


        public void EnableClearMode()
        {
            Clear = true;
            var tagDetected = new IntentFilter(NfcAdapter.ActionTagDiscovered);
            var filters = new[] { tagDetected };

            var intent = new Intent(this, GetType()).AddFlags(ActivityFlags.SingleTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, 0);

            if (_nfcAdapter == null)
            {
                var alert = new Android.App.AlertDialog.Builder(this).Create();
                alert.SetMessage("NFC is not supported on this device.");
                alert.SetTitle("NFC Unavailable");
                alert.SetButton("OK", delegate
                {

                    Toast.MakeText(this, "NFC is not supported on this device", ToastLength.Short);
                });
                alert.Show();
            }
            else
                _nfcAdapter.EnableForegroundDispatch(this, pendingIntent, filters, null);
        }

        private bool TryAndWriteToTag(Tag tag, NdefMessage ndefMessage)
        {
            var ndef = Ndef.Get(tag);
            if (ndef != null)
            {
                ndef.Connect();
                if (!ndef.IsWritable)
                {
                    Toast.MakeText(this, "Tag is read-only.", ToastLength.Short).Show();
                }
                var size = ndefMessage.ToByteArray().Length;
                if (ndef.MaxSize < size)
                {
                    Toast.MakeText(this, "Tag doesn't have enough space.", ToastLength.Short).Show();

                }

                ndef.WriteNdefMessage(ndefMessage);
                //hehe

                if (_inClearMode || Clear)
                {
                    Toast.MakeText(this, "Succesfully cleared tag.", ToastLength.Short).Show();
                    _inClearMode = false;
                }
                else
                {
                    Toast.MakeText(this, "Succesfully wrote tag.", ToastLength.Short).Show();
                    _tagWritten = true;
                }
                return true;
            }

            return false;
        }

        private bool TryAndFormatTagWithMessage(Tag tag, NdefMessage ndefMessage)
        {
            var format = NdefFormatable.Get(tag);
            if (format == null)
            {
                //DisplayMessage("Tag does not appear to support NDEF format.");
                Toast.MakeText(this, "Tag does not appear to suppord NDEF format", ToastLength.Short);
            }
            else
            {
                try
                {
                    format.Connect();
                    format.Format(ndefMessage);
                    //DisplayMessage("Tag successfully written.");
                    if (_inClearMode || Clear)
                    {
                        Toast.MakeText(this, "Tag successfully cleared", ToastLength.Short);
                    }
                    else
                    {
                        Toast.MakeText(this, "Tag successfully written", ToastLength.Short);
                        _tagWritten = true;
                    }
                    return true;
                }
                catch (IOException ioex)
                {
                    var msg = "There was an error trying to format the tag.";
                    //DisplayMessage(msg);
                    Toast.MakeText(this, msg, ToastLength.Short);
                    Log.Error(Tag, ioex, msg);
                }
            }
            return false;
        }


        protected override void OnPause()
        {
            base.OnPause();
            // App is paused, so no need to keep an eye out for NFC tags.
            if (_nfcAdapter != null)
                _nfcAdapter.DisableForegroundDispatch(this);
        }

        #region MENU METHODS
        //  akcje na klikniecie w element menu
        void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            var ft = FragmentManager.BeginTransaction();
            switch (e.MenuItem.ItemId)
            {
                case (Resource.Id.nav_home):
                    var sp = new Fragments.StoragePreview();
                    ft.Replace(Resource.Id.HomeFrameLayout, sp, "inwentaryzacja_menu");
                    ft.AddToBackStack(null);
                    ft.Commit();
                    sp.setConnection(REST);
                    break;
                case (Resource.Id.nav_messages):
                    var fragment2 = new Fragments.QuickCheckIn();
                    ft.Replace(Resource.Id.HomeFrameLayout, fragment2, "nfc_menu");
                    ft.AddToBackStack(null);
                    ft.Commit();
                    //tworzymy interfejs z fragmentem CheckIn
                    fragment2.setInterface2(this);
                    fragment2.setConnection(REST);
                    break;
                case (Resource.Id.nav_friends):
                    var fragment = new Fragments.QuickCheckOut();
                    ft.Replace(Resource.Id.HomeFrameLayout, fragment, "CheckOut");
                    ft.AddToBackStack(null);
                    ft.Commit();
                    //tworzymy interfejs z fragmentem CheckOut
                    fragment.setInterface(this);
                    fragment.setConnection(REST);
                    break;
            }
            // Close drawer
            drawerLayout.CloseDrawers();
        }

        //add custom icon to tolbar
        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.action_menu, menu);
            if (menu != null)
            {
                menu.FindItem(Resource.Id.action_refresh).SetVisible(true);
                menu.FindItem(Resource.Id.action_attach).SetVisible(false);
            }
            return base.OnCreateOptionsMenu(menu);
        }
        //define action for tolbar icon press
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    return true;
                case Resource.Id.action_attach:
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        //to avoid direct app exit on backpreesed and to show fragment from stack
        public override void OnBackPressed()
        {
            if (drawerLayout.IsDrawerOpen(GravityCompat.Start)) drawerLayout.CloseDrawers();    // zamknij menu jesli otwarte
            else
            {
                if (FragmentManager.BackStackEntryCount > 0)
                {
                    if (!(FragmentManager.FindFragmentByTag("quickmenu").IsVisible))
                    {
                        FragmentManager.PopBackStack();
                        backCount = (backCount > 0) ? backCount-- : backCount;
                    }
                    else
                    {
                        backCount++;
                        if (backCount > 1)
                        {
                            Finish();
                        }
                        Toast m = Toast.MakeText(this, "Aby wyjsc, ponownie nacisnij wstecz", ToastLength.Short);
                        m.Show();

                    }
                }
            }
        }
        #endregion

        #endregion

        public void buttonCheckIn(View v)
        {
            EnableWriteMode();
        }

        public void buttonCheckOut(View v)
        {
            EnableClearMode();
            //zwrocic na id wartosc
            //_inClearMode = true;
            //EnableWriteMode();
        }
    }
}