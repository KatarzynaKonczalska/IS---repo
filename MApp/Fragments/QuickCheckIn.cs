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
            Button saveTag = View.FindViewById<Button>(Resource.Id.button1_QuickCheckIn);
            saveTag.Click += async (sender, e) =>
            {
                //rzeczy dziej�ce si� po klikni�ciu 'ZAPISZ'
                // UNDONE: wys�anie JSON'a do bazy - z kas brac jsona do wyslania
                buttonCheckIn(view);
                TextView temp = View.FindViewById<TextView>(Resource.Id.textView10_QuickCheckIn);
                temp.Visibility = ViewStates.Visible;

                System.Json.JsonValue Data = null;
                string response = await Conn.SendData(Data);
            };

            Button generate = View.FindViewById<Button>(Resource.Id.button2_QuickCheckIn);
            generate.Click += OnClick2;

        }



        private async void OnClick2(object sender, EventArgs ea)
        //rzeczy dziej�ce si� po klikni�ciu przycisku 'GENERUJ'; Legolas -> Kasia
        {
            TextView temp = View.FindViewById<TextView>(Resource.Id.editText2_QuickCheckIn);
            temp.Text = "Generuj�...";

            //generowanie id
            // DONE: generowanie id
            //Activities.Content.id2 = await Conn.GenerateId();

            string resp;
            try
            {
                resp = await Conn.GetData(GetTypes.GetAll);
                Console.WriteLine(resp.ToString());
                //Random r = new Random();
                //Activities.Content.id2 = r.Next().ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //HttpWebRequest wr = (HttpWebRequest)WebRequest.Create("http://api.geonames.org/findNearByWeatherJSON?lat=47.7&lng=-122.5&username=demo");
            //var response = (HttpWebResponse)await wr.GetResponseAsync();
            //using (StreamReader s = new StreamReader(response.GetResponseStream()))
            //{
            //    string cont = await s.ReadToEndAsync();
            //    Console.WriteLine(cont);
            //}     
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
