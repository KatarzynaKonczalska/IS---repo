using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using MApp.REST;
using System.Json;
using System.Net;
using System.IO;

namespace MApp.Fragments
{
    public class QuickCheckIn : Fragment
    {
        CheckInInterface CinInterface;
        RESTconnection Conn;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.QuickCheckIn, container, false);
            return view;
        }
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            int i = 0;
            Button saveTag = View.FindViewById<Button>(Resource.Id.button1_QuickCheckIn);
            saveTag.Click += async (sender, e) =>
            {
                string dataToSend;
                //rzeczy dziej¹ce siê po klikniêciu 'ZAPISZ'
                // UNDONE: wys³anie JSON'a do bazy - z kas brac jsona do wyslania
                buttonCheckIn(view);
                TextView temp = View.FindViewById<TextView>(Resource.Id.textView10_QuickCheckIn);
                temp.Visibility = ViewStates.Visible;

                //if (Activities.Content._tagWritten)
                //{
                    EditText e1 = View.FindViewById<EditText>(Resource.Id.editText1_QuickCheckIn);
                    EditText e2 = View.FindViewById<EditText>(Resource.Id.editText2_QuickCheckIn);
                    EditText e3 = View.FindViewById<EditText>(Resource.Id.exitText3_QuickCheckIn);
                    EditText e4 = View.FindViewById<EditText>(Resource.Id.editText4_QuickCheckIn);

                    dataToSend = "{ \"assetName\":\"" + e1.Text + "\",\"assetAmount\":" + 12 + ",\"assetLocation\": [\"583ea7f9d6194c0c6f51fa70\", 1]" + ",\"assetDetails\":{\"NumerPartii\":\"" + e3.Text + "\",\"Producent\":\"" + e4.Text + "\"}}";
                    string response = await Conn.SendData(dataToSend, Activities.Content.id2);
                //}
                //else
                //{
                //    Toast.MakeText(Activity.ApplicationContext, "Zapis na serwer nie powiód³ siê!", ToastLength.Long);
                //}
            };

            Button generate = View.FindViewById<Button>(Resource.Id.button2_QuickCheckIn);
            generate.Click += OnClick2;
        }



        private async void OnClick2(object sender, EventArgs ea)
        //rzeczy dziej¹ce siê po klikniêciu przycisku 'GENERUJ'; Legolas -> Kasia
        {
            TextView temp = View.FindViewById<TextView>(Resource.Id.editText2_QuickCheckIn);
            temp.Text = "Generujê...";

            // DONE: generowanie id
            try
            {
                //resp = await Conn.DeleteData(123);
                //resp = await Conn.GetData(GetTypes.GetAll);
                //string json = "{\"assetName\": \"Lech piwo 4pak\",\"assetAmount\": 301,\"assetLocation\": [\"583ea7f9d6194c0c6f51fa70\",1],\"NFCTag\": \"0001\",\"assetDetails\": {\"label\":\"value\"}}";
                //resp = await Conn.SendData(json);
                //var resp = await Conn.GetData(GetTypes.GetMagazine, "583ea7f9d6194c0c6f51fa70");
                //Console.WriteLine(resp.ToString());

                var resp = await Conn.GenerateId();
                //var resp = "123212";
                Activities.Content.id2 = resp;
                temp.Text = resp;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void buttonCheckIn(View v)
        {
            CinInterface.buttonCheckIn(v);
        }

        public void setInterface2(CheckInInterface cininterface)
        {
            this.CinInterface = cininterface;
        }

        public void setConnection(RESTconnection con)
        {
            Conn = con;
        }

    }
    public interface CheckInInterface
    {
        void buttonCheckIn(View v);
    }
}
