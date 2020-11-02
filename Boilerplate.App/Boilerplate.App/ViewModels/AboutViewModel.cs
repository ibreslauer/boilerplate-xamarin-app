using Boilerplate.App.ViewModels.Base;
using Xamarin.Essentials;

namespace Boilerplate.App.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {
        public string AppVersion => $"Version {AppInfo.VersionString}";
        public AboutViewModel() : base() { }
    }
}
