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
        string id;
        public string ID
        {
            set
            {
                id = value;
                Button usunTag = View.FindViewById<Button>(Resource.Id.button1_Checkout);
                usunTag.Enabled = true;
            }
        }

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
            usunTag.Enabled = false;
            

            usunTag.Click += async (sender, e) =>
            {
                // DONE: ��danie do bazy o usuni�cie tagu
                TextView temp = View.FindViewById<TextView>(Resource.Id.textView2_Checkout);
                temp.Visibility = ViewStates.Visible;
                buttonCheckOut(view);
                string response = await Conn.DeleteData(id);
                Toast.MakeText(this.Activity, Activities.Content.id,ToastLength.Short).Show();
                TextView view1 = View.FindViewById<TextView>(Resource.Id.textView_3Checkout); //text view do id
                view1.Text = Activities.Content.id;
                //Activities.Content.id = "";
                usunTag.Enabled = false;
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
