using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace MApp.Fragments
{
    public class QuickCheckIn : Fragment
    {
        CheckInInterface CinInterface;

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
            saveTag.Click += delegate
            {
                //rzeczy dziej¹ce siê po klikniêciu 'ZAPISZ'
                buttonCheckIn(view);
                TextView temp = View.FindViewById<TextView>(Resource.Id.textView10_QuickCheckIn);
                temp.Visibility = ViewStates.Visible;
            };

            Button generate = View.FindViewById<Button>(Resource.Id.button2_QuickCheckIn);
            generate.Click += OnClick2;
        }

        private void OnClick2(object sender, EventArgs ea)
        //rzeczy dziej¹ce siê po klikniêciu przycisku 'GENERUJ'; Legolas -> Kasia
        {
            TextView temp = View.FindViewById<TextView>(Resource.Id.textView5_QuickCheckIn);
            temp.Text = "Generujê...";

            //generowanie id
            Random r = new Random();
            Activities.Content.id2 = r.Next().ToString();
        }

        public void buttonCheckIn(View v)
        {
            CinInterface.buttonCheckIn(v);
        }

        public void setInterface2(CheckInInterface cininterface)
        {
            this.CinInterface = cininterface;
        }

    }

    public interface CheckInInterface
    {
        void buttonCheckIn(View v);
    }
}
