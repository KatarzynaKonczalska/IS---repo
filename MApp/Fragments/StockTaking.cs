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
        public static string id_tag = "";
        List<string> tagi = new List<string>();
        List<JsonData> dataList = new List<JsonData>();

        public override void OnCreate(Bundle savedInstanceState)
        {
            Button showTag = View.FindViewById<Button>(Resource.Id.button1_StockTaking);
            showTag.Click += OnClick;
            showTag.Activated = false;
            for (int i = 0; i < Data.Count; i++)
            {
                var a = Data[i];
                dataList.Add(new JsonData(a["id"].ToString().Trim('"'), a["assetName"].ToString().Trim('"')));
            }
            showTag.Activated = true;
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.StockTaking, container, false);
            Activities.Content.tags.Clear();
            Activities.Content._stockTaking = true;
            return view;
        }
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            var element = from data in dataList
                          where data.ID == "122"
                          select data;

        }

        private void OnClick(object sender, EventArgs ea)
        {
            TextView temp = View.FindViewById<TextView>(Resource.Id.textView3_StockTaking);
            temp.Text = Activities.Content.tags.ElementAt(Activities.Content.tags.Count - 1) + " / " + "Null";
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
            //Activities.Content._stockTaking = false;
        }
        public void setData(JsonValue d)
        {
            Data = d;
        }

        public void addTag(string Tag)
        {
            if (!(tagi.Find(a => a == Tag).Length > 0))
                tagi.Add(Tag);

            TextView temp = View.FindViewById<TextView>(Resource.Id.textView3_StockTaking);
            temp.Text = tagi.Count + " / " + dataList.Count;
            //Toast.MakeText(Activity, Tag, ToastLength.Short).Show();
        }
    }

}
