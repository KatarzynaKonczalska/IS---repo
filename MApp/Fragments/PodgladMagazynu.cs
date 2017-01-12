using System;
using System.IO;
using System.Json;
using System.Net;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace MApp.Fragments
{
    public class PodgladMagazynu : Fragment
    {
        private ImageButton a, b;
        private RelativeLayout rl;

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
            View view = inflater.Inflate(Resource.Layout.Content, container, false);
            return view;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            a = view.FindViewById<ImageButton>(Resource.Id.imageButton1);
            b = view.FindViewById<ImageButton>(Resource.Id.imageButton2);

            string buttonClicked = "#";
            string buttonnotClicked = "#";

            //do stworzenia biblioteka(dictionary) z buttonami i ich numerami(np wg kolejnoœci dodania)

            rl = view.FindViewById<RelativeLayout>(Resource.Id.relativeLayout2);
            rl.Visibility = ViewStates.Invisible;

            //chwilowe rozwi¹zanie zamiast dictionary
            int lastClickedButton = 0;

            a.Click += delegate // zuniwersalizowaæ dla ka¿dego przycisku
            {
                if (lastClickedButton == 0)
                {
                    lastClickedButton = 1;
                }

                if (lastClickedButton == 1 && rl.Visibility != ViewStates.Visible) // poprzedni klik by³ na ten przycisk i jest niewidoczny kontent
                {
                    // poka¿ kontent
                    a.SetBackgroundColor(Color.Aqua);
                    rl.Visibility = ViewStates.Visible;

                    _LoadValues(_OpenLocalJson(@"jsonLocal.json"), view.FindViewById<TextView>(Resource.Id.textView4));
                }
                else if (lastClickedButton == 1 && rl.Visibility == ViewStates.Visible) // poprzedni klik by³ na ten przycisk i jest niewidoczny kontent
                {
                    // schowaj kontent
                    rl.Visibility = ViewStates.Invisible;
                }
                else if (lastClickedButton != 1 && rl.Visibility != ViewStates.Visible) // poprzedni klik by³ inny przycisk i jest niewidoczny kontent
                {
                    // zmieñ kontent i poka¿

                    // zmieniam kontent...
                    rl.Visibility = ViewStates.Visible;
                    _LoadValues(_OpenLocalJson(@"jsonLocal.json"), view.FindViewById<TextView>(Resource.Id.textView4));
                }
                else if (lastClickedButton != 1 && rl.Visibility == ViewStates.Visible) // poprzedni klik by³ inny przycisk i jest widoczny kontent
                {
                    // schowaj kontent, zmieñ kontent i poka¿

                    rl.Visibility = ViewStates.Invisible;
                    //zmieniam kontnet...
                    rl.Visibility = ViewStates.Visible;
                    _LoadValues(_OpenLocalJson(@"jsonLocal.json"), view.FindViewById<TextView>(Resource.Id.textView4));
                }
                lastClickedButton = 1;
            };

            b.Click += delegate
            {
                if (lastClickedButton == 0)
                {
                    lastClickedButton = 2;
                }
                if (lastClickedButton == 2 && rl.Visibility != ViewStates.Visible) // poprzedni klik by³ na ten przycisk i jest niewidoczny kontent
                {
                    // poka¿ kontent
                    rl.Visibility = ViewStates.Visible;
                    _LoadValues(_OpenLocalJson(@"jsonLocal2.json"), view.FindViewById<TextView>(Resource.Id.textView4));
                }
                else if (lastClickedButton == 2 && rl.Visibility == ViewStates.Visible) // poprzedni klik by³ na ten przycisk i jest niewidoczny kontent
                {
                    // schowaj kontent
                    rl.Visibility = ViewStates.Invisible;
                }
                else if (lastClickedButton != 2 && rl.Visibility != ViewStates.Visible) // poprzedni klik by³ inny przycisk i jest niewidoczny kontent
                {
                    // zmieñ kontent i poka¿

                    // zmieniam kontent...
                    rl.Visibility = ViewStates.Visible;
                    _LoadValues(_OpenLocalJson(@"jsonLocal2.json"), view.FindViewById<TextView>(Resource.Id.textView4));
                }
                else if (lastClickedButton != 2 && rl.Visibility == ViewStates.Visible) // poprzedni klik by³ inny przycisk i jest widoczny kontent
                {
                    // schowaj kontent, zmieñ kontent i poka¿

                    rl.Visibility = ViewStates.Invisible;
                    //zmieniam kontnet...
                    rl.Visibility = ViewStates.Visible;
                    _LoadValues(_OpenLocalJson(@"jsonLocal2.json"), view.FindViewById<TextView>(Resource.Id.textView4));
                }
                lastClickedButton = 2;
            };
        }
    }
}