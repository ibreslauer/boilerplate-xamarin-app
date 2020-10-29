using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using System.Threading.Tasks;

namespace Boilerplate.App.Droid
{
    [Activity(Theme = "@style/BoilerplateTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
        }

        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => {
                StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            });
            startupWork.Start();
        }

        public override void OnBackPressed() { }
    }
}