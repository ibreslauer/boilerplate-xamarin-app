using Boilerplate.App.CrossPlatform;
using Boilerplate.App.Droid.Dependencies;
using Xamarin.Forms;
using Application = Android.App.Application;
using SecureSettings = Android.Provider.Settings.Secure;

[assembly: Dependency(typeof(DeviceIdService))]
namespace Boilerplate.App.Droid.Dependencies
{
    public class DeviceIdService : IDeviceIdService
    {
        public string DeviceId => SecureSettings.GetString(
                Application.Context.ContentResolver,
                SecureSettings.AndroidId);
    }
}