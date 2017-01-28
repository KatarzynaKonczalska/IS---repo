using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using MApp.REST;
using System.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MApp.Fragments
{
    public class StockTaking : Fragment
    {
        JsonValue Data;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.StockTaking, container, false);
            return view;
        }
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            Button showTag = View.FindViewById<Button>(Resource.Id.button1_StockTaking);
            showTag.Click += OnClick;
            Console.WriteLine(Data.ToString());

            //na liste
            //List<int> dataList = new List<int>();
            //for (int i = 0; i < Data.Count-1; i++)
            //{
            //    var a = Data[i]["id"];
            //    string b = a.ToString();
            //    dataList.Add(int.Parse(b));
            //}
            //Console.WriteLine(dataList);
        }

        private void OnClick(object sender, EventArgs ea)
        {
            TextView temp = View.FindViewById<TextView>(Resource.Id.textView4_StockTaking);
            temp.Visibility = ViewStates.Visible;
        }
        public void setData(JsonValue d)
        {
            Data = d;
        }
    }
}
