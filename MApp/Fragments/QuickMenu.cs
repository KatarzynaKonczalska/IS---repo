using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace MApp.Fragments
{
    public class QuickMenu : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.QuickMenu, container, false);
            return view;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            Button inwentaryzacja = View.FindViewById<Button>(Resource.Id.inwentaryzacja),
                przyjmij = View.FindViewById<Button>(Resource.Id.przyjecie),
                wydaj = View.FindViewById<Button>(Resource.Id.wydanie);

            inwentaryzacja.Click += delegate
            {
                //var nowy = new SektorDetails();
                //var fm = FragmentManager.BeginTransaction();
                //fm.Replace(Resource.Id.HomeFrameLayout, nowy, "inwentaryzacja");
                //fm.AddToBackStack(null);
                //fm.Commit();
            };

            przyjmij.Click += delegate
            {
                var nowy = new QuickCheckIn();
                var fm = FragmentManager.BeginTransaction();
                fm.Replace(Resource.Id.HomeFrameLayout, nowy, "przyjmij");
                fm.AddToBackStack(null);
                fm.Commit();
            };

            wydaj.Click += delegate
            {
                var nowy = new QuickCheckOut();
                var fm = FragmentManager.BeginTransaction();
                fm.Replace(Resource.Id.HomeFrameLayout, nowy, "wydaj");
                fm.AddToBackStack(null);
                fm.Commit();
            };
        }
    }
}