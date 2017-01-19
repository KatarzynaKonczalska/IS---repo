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
        }
    }
}