using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using MApp.REST;

namespace MApp.Fragments
{
    public class QuickCheckOut : Fragment
    {
        CheckOutInterface CoutInterface;
        int passedInt;
        RESTconnection Conn;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.QuickCheckOut, container, false);
            return view;
        }
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            Button usunTag = View.FindViewById<Button>(Resource.Id.button1_Checkout);
            usunTag.Click += async (sender, e) =>
            {
                // DONE: ¿¹danie do bazy o usuniêcie tagu
                TextView temp = View.FindViewById<TextView>(Resource.Id.textView2_Checkout);
                temp.Visibility = ViewStates.Visible;
                buttonCheckOut(view);
                string response = await Conn.DeleteData(int.MaxValue); // przykladowe id
            };
        }

        public void buttonCheckOut(View v)
        {
            CoutInterface.buttonCheckOut(v);
        }

        public void setInterface(CheckOutInterface coutinterface)
        {
            this.CoutInterface = coutinterface;
        }

        public void setConnection(RESTconnection con)
        {
            Conn = con;
        }
    }

    public interface CheckOutInterface
    {
        void buttonCheckOut(View v);
    }
}
