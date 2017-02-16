using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Json;
using System.Collections.Generic;

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
        public static string id_tag = "";
        List<string> tagi = new List<string>();
        List<JsonData> dataList = new List<JsonData>();
        TextView Gtemp, Gtemp2;
        Button Gbut;

        public override void OnCreate(Bundle savedInstanceState)
        {
            //Button showTag = View.FindViewById<Button>(Resource.Id.button1_StockTaking);
            //showTag.Click += OnClick;
            //showTag.Activated = false;
            for (int i = 0; i < Data.Count; i++)
            {
                var a = Data[i];
                dataList.Add(new JsonData(a["id"].ToString().Trim('"'), a["assetName"].ToString().Trim('"')));
            }
            //showTag.Activated = true;

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
            TextView temp = View.FindViewById<TextView>(Resource.Id.textView3_StockTaking);
            temp.Text = "0 / " + dataList.Count;

            Button pokazRoznice = View.FindViewById<Button>(Resource.Id.button1_StockTaking);
            pokazRoznice.Click += PokazRoznice;

            Gtemp = View.FindViewById<TextView>(Resource.Id.textView3_StockTaking);
            Gbut = View.FindViewById<Button>(Resource.Id.button1_StockTaking);
            Gtemp2 = View.FindViewById<TextView>(Resource.Id.textView4_StockTaking);
        }

        private void OnClick(object sender, EventArgs ea)
        {
            TextView temp = View.FindViewById<TextView>(Resource.Id.textView3_StockTaking);

            Toast.MakeText(this.Activity, Activities.Content.id_inw, ToastLength.Short).Show();
        }

        private void PokazRoznice(object sender, EventArgs ea)
        {
            string tmp = "";
            foreach (var item in dataList)
            {
                if (tagi.Find(a => a == item.ID) == null)
                {
                    tmp += item.Nazwa + "\n";
                }
            }
            Gtemp2.Text = tmp;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        public override void OnPause()
        {
            base.OnPause();
            //Activities.Content._stockTaking = false;
        }
        public void setData(JsonValue d)
        {
            Data = d;
        }

        public void addTag(string Tag)
        {
            //if (tagi.Find(a => a == Tag).Length == 0)
            if (dataList.Find(a => a.ID == Tag) != null)
                tagi.Add(Tag);

            Gtemp.Text = tagi.Count + " / " + dataList.Count;
            if(dataList.Count==tagi.Count)
            {
                Gbut.Visibility = ViewStates.Gone;
                Gtemp2.Visibility = ViewStates.Gone;
                Gtemp.Text = "Zakoñczono";
                tagi.Clear();
            }
            //Toast.MakeText(Activity, Tag, ToastLength.Short).Show();
        }
    }

}
