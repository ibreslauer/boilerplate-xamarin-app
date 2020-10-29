using Xamarin.Forms;

using Plugin.CurrentActivity;

using Boilerplate.App.CrossPlatform;
using Boilerplate.App.Droid.Dependencies;

[assembly: Dependency(typeof(CloseAppService))]
namespace Boilerplate.App.Droid.Dependencies
{
    public class CloseAppService : ICloseAppService
    {
        public void Close()
        {
            CrossCurrentActivity.Current.Activity?.FinishAffinity();
        }
    }
}