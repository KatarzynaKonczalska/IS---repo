using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using MApp.REST;
using System.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace MApp.Fragments
{
    public class StockTaking : Fragment
    {
        JsonValue Data;
        public static string id_tag = "";
        List<string> tagi;

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
            //Console.WriteLine(Data.ToString());

            
            Activities.Content._stockTaking = true;


            //czytanie NFC
            //if(Activities.Content.id.Length>0)
            //{
            //    Toast.MakeText(this.Activity, Activities.Content.id, ToastLength.Short).Show();
            //    //wpisaæ dodanie do listy
            //    Activities.Content.id = "";
            //}


            //na liste
            //List<string> dataList = new List<string>();
            //for (int i = 0; i < Data.Count; i++)
            //{
            //    var a = Data[i]["id"];
            //    dataList.Add(a.ToString().Trim('"'));
            //}
            //foreach (var item in dataList)
            //{
            //    Console.WriteLine(item);
            //}



        }

        private void OnClick(object sender, EventArgs ea)
        {
            TextView temp = View.FindViewById<TextView>(Resource.Id.textView3_StockTaking);
            temp.Text = tags.ElementAt(tags.Count - 1) + " / " + "Null";
            Toast.MakeText(this.Activity, Activities.Content.id_inw, ToastLength.Short).Show();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            Activities.Content._stockTaking = false;
        }

        public override void OnPause()
        {
            base.OnPause();
            Activities.Content._stockTaking = false;
        }
        public void setData(JsonValue d)
        {
            Data = d;
        }
    }
}
