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
        class JsonData
        {
            public JsonData(string id, string nazwa)
            {
                ID = id; Nazwa = nazwa;
            }
            public string ID { get; private set; }
            public string Nazwa { get; private set; }
        }

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

            //czytanie NFC
            if(Activities.Content.id.Length>0)
            {
                Toast.MakeText(this.Activity, Activities.Content.id, ToastLength.Short).Show();
                //wpisaæ dodanie do listy
                Activities.Content.id = "";
            }


            //na liste
            List<JsonData> dataList = new List<JsonData>();
            for (int i = 0; i < Data.Count; i++)
            {
                var a = Data[i];
                dataList.Add(new JsonData(a["id"].ToString().Trim('"'), a["assetName"].ToString().Trim('"')));
            }

            var element = from data in dataList
                          where data.ID == "122"
                          select data;

            //foreach (var item in dataList)
            //{
            //    Console.WriteLine(item);
            //}
            
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
