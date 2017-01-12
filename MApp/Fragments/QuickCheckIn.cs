using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace MApp.Fragments
{
    public class QuickCheckIn : Fragment
    {
        int passedInt;

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
            saveTag.Click += OnClick;

            Button generate = View.FindViewById<Button>(Resource.Id.button2_QuickCheckIn);
            generate.Click += OnClick2;
        }

        public void TakeInt(int value)
        {
            passedInt = value;
        }

        private void OnClick(object sender, EventArgs ea)
        //rzeczy dziej¹ce siê po klikniêciu 'ZAPISZ'; Legolas -> Kasia
        {
            TextView temp = View.FindViewById<TextView>(Resource.Id.textView10_QuickCheckIn);
            temp.Visibility = ViewStates.Visible;
        }
        private void OnClick2(object sender, EventArgs ea)
        //rzeczy dziej¹ce siê po klikniêciu przycisku 'GENERUJ'; Legolas -> Kasia
        {
            TextView temp = View.FindViewById<TextView>(Resource.Id.textView5_QuickCheckIn);
            temp.Text = "Generujê...";
        }
    }
}
