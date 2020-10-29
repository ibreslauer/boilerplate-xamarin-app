using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Boilerplate.App.CrossPlatform
{
    public class AppRuntime
    {
        public readonly static Lazy<string> DEVICE_MAKE = new Lazy<string>(() => GetDeviceMake());
        public readonly static Lazy<string> DEVICE_ID = new Lazy<string>(() => DependencyService.Get<IDeviceIdService>().DeviceId);
        public readonly static Lazy<string> APP_VERSION = new Lazy<string>(() => DependencyService.Get<IAppVersionService>().VersionName);

        private static string GetDeviceMake()
        {
            return DeviceInfo.Manufacturer.ToUpper();
        }
    }
}
