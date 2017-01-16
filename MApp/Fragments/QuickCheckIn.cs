using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using MApp.REST;

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
                //rzeczy dziej¹ce siê po klikniêciu 'ZAPISZ'
                // UNDONE: wys³anie JSON'a do bazy - z kas brac jsona do wyslania
                buttonCheckIn(view);
                TextView temp = View.FindViewById<TextView>(Resource.Id.textView10_QuickCheckIn);
                temp.Visibility = ViewStates.Visible;

                System.Json.JsonValue Data = null;
                string response = await Conn.SendData(GetTypes.SendAsset, Data);
            };

            Button generate = View.FindViewById<Button>(Resource.Id.button2_QuickCheckIn);
            generate.Click += OnClick2;
        }

        private async void OnClick2(object sender, EventArgs ea)
        //rzeczy dziej¹ce siê po klikniêciu przycisku 'GENERUJ'; Legolas -> Kasia
        {
            TextView temp = View.FindViewById<TextView>(Resource.Id.textView5_QuickCheckIn);
            temp.Text = "Generujê...";

            // DONE: generowanie id
            Activities.Content.id2 = await Conn.GenerateId();
            
            //Random r = new Random();
            //Activities.Content.id2 = r.Next().ToString();
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
