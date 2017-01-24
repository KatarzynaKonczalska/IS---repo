using System;
using System.Timers;
using System.IO;
using System.Json;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using System.Collections.Generic;
using Android.Animation;
using static Android.App.ActionBar;
using Android.Views.Animations;
using System.Collections;
using System.Threading;
using Android.Transitions;
using MApp.REST;

namespace MApp.Fragments
{

    public class StoragePreview : Fragment
    {
        RESTconnection Conn;
        int lastClicked = -1;
        int columnMax = 6;
        Color colorClicked = Color.Rgb(45, 126, 255);
        Color colorNotClicked = Color.Rgb(45, 190, 255);
        int durationInMiliseconds = 1000;
        int animUsed = 0;
        
        List<int> sectorFill = new List<int>();
        Dictionary<int, ImageButton> sectorList = new Dictionary<int, ImageButton>();
        List<RelativeLayout> relativeLayoutCollection = new List<RelativeLayout>();
        List<List<TextView>> CategoryLists = new List<List<TextView>>();

        private int ConvertPixelsToDp(float pixelValue)
        {
            var dp = (int)((pixelValue) / Resources.DisplayMetrics.Density);
            return dp;
        }

        private int ConvertDpToPixels(float DpValue)
        {
            int pixels = (int)((DpValue) * Resources.DisplayMetrics.Density);
            return pixels;
        }

        private void SectorClick(object sender, EventArgs ea)
        {
            ProgressBar progress = View.FindViewById<ProgressBar>(Resource.Id.fill_1SP);
            ImageButton myButton = sender as ImageButton;
            int ButtonNum = (int)myButton.Tag;
            RelativeLayout ll2 = View.FindViewById<RelativeLayout>(Resource.Id.LinearLayout_2SP);
            RelativeLayout ll3 = View.FindViewById<RelativeLayout>(Resource.Id.LinearLayout_3SP); 
            RelativeLayout RLCurrent = relativeLayoutCollection.ElementAt(ButtonNum);
            RelativeLayout RLLast;

            RelativeLayout.LayoutParams param2 = new RelativeLayout.LayoutParams(ll3.LayoutParameters);
            param2.SetMargins(0, ll2.Bottom, 0,0);
            ll3.SetPadding(0, 0, 0, ConvertDpToPixels(40));
            ll3.LayoutParameters = param2;

            if (lastClicked == -1)
            {
                lastClicked = ButtonNum;
                progress.Progress = sectorFill.ElementAt(ButtonNum);
                RLCurrent.Visibility = ViewStates.Visible;
            }

            ImageButton lastButton = sectorList.First(s => s.Key == lastClicked).Value;
            RLLast = relativeLayoutCollection.ElementAt(lastClicked);

            if (lastClicked == ButtonNum && ll3.Visibility != ViewStates.Visible)
            // poprzedni klik by³ na ten przycisk i jest niewidoczny kontent
            {
                // poka¿ kontent
                myButton.SetImageResource(Resource.Drawable.buttonLight);
                ll3.Visibility = ViewStates.Visible;
                _TranslateAnimationMethod_1(ll3, ButtonNum);
            }
            else if (lastClicked == ButtonNum && ll3.Visibility == ViewStates.Visible)
            // poprzedni klik by³ na ten przycisk i jest widoczny kontent
            {
                // schowaj kontent
                myButton.SetImageResource(Resource.Drawable.buttonDark);
                _TranslateAnimationMethod_2(ll3, ButtonNum);
                
            }
            else if (lastClicked != ButtonNum && ll3.Visibility != ViewStates.Visible)
            // poprzedni klik by³ inny przycisk i jest niewidoczny kontent
            {
                // zmieñ kontent i poka¿
                lastButton.SetImageResource(Resource.Drawable.buttonDark);
                myButton.SetImageResource(Resource.Drawable.buttonLight);
                // zmieniam kontent...
                RLLast.Visibility = ViewStates.Gone;
                RLCurrent.Visibility = ViewStates.Visible;
                //pokazuje
                ll3.Visibility = ViewStates.Visible;
                _TranslateAnimationMethod_3(ll3, ButtonNum);

            }
            else if (lastClicked != ButtonNum && ll3.Visibility == ViewStates.Visible)
            // poprzedni klik by³ inny przycisk i jest widoczny kontent
            {
                // schowaj kontent, zmieñ kontent i poka¿
                lastButton.SetImageResource(Resource.Drawable.buttonDark);
                _TranslateAnimationMethod_4(ll3, ButtonNum);

            }
            lastClicked = ButtonNum;
        }

        private void _TranslateAnimationMethod(View viewToAnimate, int fromXDelta, int toXDelta, int fromYDelta, int toYDelta, int durationInMiliseconds, bool changeContent, int ButtonNum, bool isGone)
        {
            Animation animation = new TranslateAnimation(fromXDelta, toXDelta, fromYDelta, toYDelta);
            animation.Duration = durationInMiliseconds;
            animation.FillAfter = true;
            viewToAnimate.StartAnimation(animation);

            if (isGone)
            {
                animation.AnimationEnd += delegate
                {
                    viewToAnimate.Visibility = ViewStates.Gone;
                };
            }

            if (changeContent)
            {
                animation.AnimationEnd += delegate
                {
                    RelativeLayout RLLast = relativeLayoutCollection.ElementAt(lastClicked);
                    RelativeLayout RLCurrent = relativeLayoutCollection.ElementAt(ButtonNum);
                    ProgressBar progress = View.FindViewById<ProgressBar>(Resource.Id.fill_1SP);
                    sectorList.ElementAt(ButtonNum).Value.SetImageResource(Resource.Drawable.buttonLight);

                    viewToAnimate.Visibility = ViewStates.Gone;

                    foreach (var item in relativeLayoutCollection)
                    {
                        item.Visibility = ViewStates.Gone;
                    }
                    RLCurrent.Visibility = ViewStates.Visible;
                    progress.Progress = sectorFill.ElementAt(ButtonNum);

                    viewToAnimate.Visibility = ViewStates.Visible;

                    Animation animation2 = new TranslateAnimation(0, 0, -viewToAnimate.Bottom, 0);
                    animation2.Duration = durationInMiliseconds;
                    animation2.FillAfter = true;
                    viewToAnimate.StartAnimation(animation2);
                };
            }
        }

        private void _TranslateAnimationMethod_1(View viewToAnimate, int ButtonNum)
        {
            RelativeLayout ll2 = View.FindViewById<RelativeLayout>(Resource.Id.LinearLayout_2SP);
            if (animUsed == 0)
            {
                viewToAnimate.Visibility = ViewStates.Visible;
                int i = viewToAnimate.Bottom;
                /*Animation animation = new TranslateAnimation(0, 0, -(), ll2.Bottom);
                animation.Duration = durationInMiliseconds;
                animation.FillAfter = true;
                viewToAnimate.StartAnimation(animation);
                animUsed++;
                */
            }
            else
            {
                viewToAnimate.Visibility = ViewStates.Visible;
                Animation animation = new TranslateAnimation(0, 0, -viewToAnimate.Bottom, 0);
                animation.Duration = durationInMiliseconds;
                animation.FillAfter = true;
                viewToAnimate.StartAnimation(animation);
            }
            
        }
        private void _TranslateAnimationMethod_2(View viewToAnimate, int ButtonNum)
        {
            Animation animation = new TranslateAnimation(0, 0, 0, -(viewToAnimate.Bottom));
            animation.Duration = durationInMiliseconds;
            animation.FillAfter = true;
            viewToAnimate.StartAnimation(animation);

            animation.AnimationEnd += delegate
            {
                viewToAnimate.Visibility = ViewStates.Gone;
            };

        }
        private void _TranslateAnimationMethod_3(View viewToAnimate, int ButtonNum)
        {
            Animation animation = new TranslateAnimation(0, 0, -(viewToAnimate.Bottom), 0);
            animation.Duration = durationInMiliseconds;
            animation.FillAfter = true;
            viewToAnimate.StartAnimation(animation);
        } 
        private void _TranslateAnimationMethod_4(View viewToAnimate, int ButtonNum)
        {
            Animation animation = new TranslateAnimation(0, 0, 0, -(viewToAnimate.Bottom));
            animation.Duration = durationInMiliseconds;
            animation.FillAfter = true;
            viewToAnimate.StartAnimation(animation);

            animation.AnimationEnd += delegate
                {
                    RelativeLayout RLLast = relativeLayoutCollection.ElementAt(lastClicked);
                    RelativeLayout RLCurrent = relativeLayoutCollection.ElementAt(ButtonNum);
                    ProgressBar progress = View.FindViewById<ProgressBar>(Resource.Id.fill_1SP);
                    sectorList.ElementAt(ButtonNum).Value.SetImageResource(Resource.Drawable.buttonLight);

                    viewToAnimate.Visibility = ViewStates.Gone;

                    foreach (var item in relativeLayoutCollection)
                    {
                        item.Visibility = ViewStates.Gone;
                    }
                    RLCurrent.Visibility = ViewStates.Visible;
                    progress.Progress = sectorFill.ElementAt(ButtonNum);

                    viewToAnimate.Visibility = ViewStates.Visible;

                    Animation animation2 = new TranslateAnimation(0, 0, -(viewToAnimate.Bottom), 0);
                    animation2.Duration = durationInMiliseconds;
                    animation2.FillAfter = true;
                    viewToAnimate.StartAnimation(animation2);
                };
            
        }

        private void _CreateSector() // do zmian po otrzymaniu ostatecznych jsonów.
        {
            
            int columnCounter = sectorList.Count;
            int rowCounter = 0;

            while (columnCounter >= columnMax)
            {
                columnCounter = columnCounter - columnMax;
                rowCounter++;
            }

            int fill = 50 + sectorList.Count;// pobrane z jsona
            int devW = Resources.DisplayMetrics.WidthPixels;
            int devH = Resources.DisplayMetrics.HeightPixels;
            GridLayout gl = View.FindViewById<GridLayout>(Resource.Id.gridLayout_1SP);
            RelativeLayout rl = View.FindViewById<RelativeLayout>(Resource.Id.relativeLayout_2SP);
            ImageButton buttonNew = new ImageButton(Activity.ApplicationContext);
            TextView tv = View.FindViewById<TextView>(Resource.Id.textView_1SP);
            sectorFill.Add(fill);

            RelativeLayout newRL = new RelativeLayout(Activity.ApplicationContext);
            RelativeLayout.LayoutParams RLparams = new RelativeLayout.LayoutParams(100,100);
            RLparams.Width = LayoutParams.MatchParent;
            RLparams.TopMargin = ConvertDpToPixels(25);
            
            buttonNew.SetMinimumHeight((int)(5));
            buttonNew.SetMinimumWidth((int)(5));
            buttonNew.SetImageResource(Resource.Drawable.buttonDark);
            buttonNew.SetBackgroundColor(Color.White);
            buttonNew.Click += SectorClick;
            buttonNew.Tag = sectorList.Count;
            sectorList.Add(sectorList.Count, buttonNew);

            GridLayout.Spec col = GridLayout.InvokeSpec(columnCounter, GridLayout.Center);// kolumna pobrana z jsona
            GridLayout.Spec row = GridLayout.InvokeSpec(rowCounter, GridLayout.Center);// wiersz pobrany z jsona
            gl.AddView(buttonNew, new GridLayout.LayoutParams(row, col));

            List<TextView> categoryList = new List<TextView>();
            // _LoadValues(_OpenLocalJson(@"jsonLocal2.json"), textNew);// pobierz kategorie - nazwy

            for (int j = 0; j < 20; j++)
            {
                TextView textView = new TextView(Activity.ApplicationContext);
                textView.Clickable = true;
                textView.Click += ItemClick;
                categoryList.Add(textView);
            }

            int k = 0;
            foreach (TextView item in categoryList)
            {
                item.Text = "Kategoria_" + (categoryList.Count * (sectorList.Count-1) + k);
                item.SetWidth(rl.RootView.MeasuredWidth);
                item.SetHeight(ConvertDpToPixels(25));
                item.TranslationY = k * ConvertDpToPixels(25);
                item.SetTextColor(Color.Black);
                item.Gravity = GravityFlags.Center;
                item.SetTextSize(Android.Util.ComplexUnitType.Px, ConvertDpToPixels(20));
                newRL.AddView(item);
                k++;
            }

            int height = ConvertDpToPixels(25 * categoryList.Count);
            RLparams.Height = height + ConvertDpToPixels(12);
            newRL.Visibility = ViewStates.Gone;
            newRL.LayoutParameters = RLparams;
            rl.AddView(newRL);
            relativeLayoutCollection.Add(newRL);
            
            CategoryLists.Add(categoryList);
        }

        private void ItemClick(object sender, EventArgs e)
        {
            //odpalenie nastêpnego fragmentu
        }

        private JsonValue _OpenLocalJson(string name)
        {
            using (var open = Activity.Assets.Open(name))
            {
                JsonValue file = JsonObject.Load(open);
                return file;
            }
        }

        private async Task<JsonValue> _OpenUrlJson(string url)
        {
            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));

                    // Return the JSON document:
                    return jsonDoc;
                }
            }
        }

        private void _LoadValues(JsonValue plik, TextView pole)
        {
            string d = "";
            for (int i = 0; i < 2; i++)
            {
                JsonValue v2 = plik["" + i];
                JsonValue v3 = v2["Produkty"];
                for (int j = 1; j <= 6; j++)
                {
                    d += v3["" + j]["Nazwa"].ToString();
                    d += ", ";
                }
            }
            pole.Text = d;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            //return base.OnCreateView(inflater, container, savedInstanceState);
            View view = inflater.Inflate(Resource.Layout.StoragePreview, container, false);
            return view;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            
        }

        public override void OnStart()
        {
            base.OnStart();
            sectorList.Clear();
            sectorFill.Clear();
            relativeLayoutCollection.Clear();
            foreach (var item in CategoryLists)
            {
                item.Clear();
            }
            CategoryLists.Clear();
            lastClicked = -1;
            animUsed = 0;

            GridLayout gl = View.FindViewById<GridLayout>(Resource.Id.gridLayout_1SP);
            RelativeLayout ll1 = View.FindViewById<RelativeLayout>(Resource.Id.linearLayout_1SP);
            RelativeLayout ll2 = View.FindViewById<RelativeLayout>(Resource.Id.LinearLayout_2SP);
            RelativeLayout ll3 = View.FindViewById<RelativeLayout>(Resource.Id.LinearLayout_3SP);
            TextView t = View.FindViewById<TextView>(Resource.Id.StorageLocalization);

            Button stockTaking = View.FindViewById<Button>(Resource.Id.button1_StoragePreview);
            stockTaking.Click += async (sender, e) =>
            {
                var data = await Conn.GetData(GetTypes.GetSectorAssets, "1");
                var nowy = new StockTaking();
                var fm = FragmentManager.BeginTransaction();
                fm.Replace(Resource.Id.HomeFrameLayout, nowy, "inwentaryzacja");
                fm.AddToBackStack(null);
                fm.Commit();
                nowy.setData(data);
            };

            RelativeLayout.LayoutParams param = new RelativeLayout.LayoutParams(gl.LayoutParameters);
            param.Width = ViewGroup.LayoutParams.MatchParent;
            param.Height = ViewGroup.LayoutParams.WrapContent;
            gl.RowCount = columnMax;
            gl.ColumnCount = columnMax;
            gl.SetBackgroundColor(Color.White);
            gl.LayoutParameters = param;



            ll3.Visibility = ViewStates.Gone;
            for (int j = 0; j < 16; j++)
                _CreateSector();
        }
        public void setConnection(RESTconnection con)
        {
            Conn = con;
        }
    }
}