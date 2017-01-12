using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Preferences;

namespace MApp.Activities
{
    [Activity(Label = "LogIn", NoHistory = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class LogIn : Activity
    {
        #region Private
        private bool _LogInToServer()
        {
            return true;
        }
        #endregion

        #region Override
        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LogIn);

            #region Preferences
            ISharedPreferences Preferences = PreferenceManager.GetDefaultSharedPreferences(this);
            ISharedPreferencesEditor Editor = Preferences.Edit();
            #endregion

            FindViewById<Button>(Resource.Id.login).Click += (sender, eventargs) =>
            {
                if (_LogInToServer())
                {
                    Editor.PutBoolean("loggedin", true);
                    Editor.Commit();
                    Intent page_Content = new Intent(this, typeof(Content));
                    page_Content.AddFlags(ActivityFlags.NewDocument);
                    StartActivity(typeof(Content));
                }
                else
                {
                    Editor.PutBoolean("loggedin", false);
                    // Mbox 'blendne dane sprobuj ponownie'
                }
            };

            #endregion
        }
    }
}