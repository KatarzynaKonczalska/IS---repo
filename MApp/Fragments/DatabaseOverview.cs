using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace MApp.Fragments
{
    public class DatabaseOverview : Fragment
    {
        GridView gridView, gridView2;
        string[] gridViewString = {
            "Produkt1","Produkt2","Produkt3",
            "Produkt4","Produkt5","Produkt6",
            "Produkt7","Produkt8","Produkt9",
            "Produkt10","Produkt11","Produkt12",
            "Produkt13","Produkt14","Produkt15",
            "Produkt16","Produkt17","Produkt18",
            "Produkt19","Produkt20","Produkt21",
            "Produkt22","Produkt23","Produkt24",
            "Produkt25","Produkt26","Produkt27",
            "Produkt28","Produkt29","Produkt30"
        };

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            //View view = inflater.Inflate(Resource.Layout.DatabaseOverview, container, false);
            View view = inflater.Inflate(Resource.Layout.DatabaseOverview, container, false);


            return view;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {

            OverviewGridAdapter adapter = new OverviewGridAdapter(this.Activity, gridViewString, gridViewString);
            gridView = View.FindViewById<GridView>(Resource.Id.grid_view_image_text);
            gridView.Adapter = adapter;
            gridView.ItemClick += (s, e) =>
            {
                Toast.MakeText(this.Activity, "GridView Item: " + gridViewString[e.Position], ToastLength.Short).Show();
            };

            base.OnViewCreated(view, savedInstanceState);
        }
    }

    public class OverviewGridAdapter : BaseAdapter
    {
        private Context context;
        private string[] gridViewString;
        private string[] gridViewString2;

        public OverviewGridAdapter(Context context, string[] gridViewstr, string[] gridViewstr2)
        {
            this.context = context;
            gridViewString = gridViewstr;
            gridViewString2 = gridViewstr2;
        }

        public override int Count
        {
            get
            {
                return gridViewString.Length;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view;
            LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
            if(convertView == null)
            {
                view = new View(context);
                view = inflater.Inflate(Resource.Layout.Raw2, null);
                TextView txtView = view.FindViewById<TextView>(Resource.Id.textView);
                TextView txtView2 = view.FindViewById<TextView>(Resource.Id.textView111);
                txtView.Text = gridViewString[position];
                txtView2.Text = gridViewString2[position];
            }
            else
            {
                view = (View)convertView;
            }
            return view;
        }
    }
    
}