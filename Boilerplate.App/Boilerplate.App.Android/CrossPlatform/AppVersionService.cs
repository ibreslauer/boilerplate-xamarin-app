using Android.Content.PM;
using Boilerplate.App.CrossPlatform;
using Boilerplate.App.Droid.Dependencies;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppVersionService))]
namespace Boilerplate.App.Droid.Dependencies
{
    public class AppVersionService : IAppVersionService
    {
        private readonly PackageInfo _appInfo;
        public AppVersionService()
        {
            var context = Android.App.Application.Context;
            _appInfo = context.PackageManager.GetPackageInfo(context.PackageName, PackageInfoFlags.MetaData);
        }

        public string VersionName => _appInfo?.VersionName;
        public long VersionCode => _appInfo.LongVersionCode;
    }
}