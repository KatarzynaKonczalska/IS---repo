using Android.App;
using Android.OS;
using Android.Content;
using Android.Views;
using Android.Preferences;

namespace MApp.Activities
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon", NoHistory = true)]
    public class MainActivity : Activity
    {
        #region Override
        protected override void OnCreate(Bundle bundle)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);

            base.OnCreate(bundle);

            #region Preferences
            ISharedPreferences Manager = PreferenceManager.GetDefaultSharedPreferences(this);
            bool Preference_LogIn = Manager.GetBoolean("loggedin", false);
            #endregion

            #region CheckPreferences
            if (Preference_LogIn)
            {
                Intent page_Content = new Intent(this, typeof(Content));
                page_Content.AddFlags(ActivityFlags.NewDocument);
                StartActivity(typeof(Content));
            }
            else
            {
                Intent page_LogIn = new Intent(this, typeof(LogIn));
                page_LogIn.AddFlags(ActivityFlags.NewDocument);
                StartActivity(typeof(LogIn));
            }
            #endregion
        }
        #endregion
    }
}


