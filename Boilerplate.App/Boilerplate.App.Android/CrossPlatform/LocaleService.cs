using Boilerplate.App.CrossPlatform;
using Boilerplate.App.Droid.Dependencies;
using System.Globalization;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocaleService))]
namespace Boilerplate.App.Droid.Dependencies
{
    public class LocaleService : ILocaleService
    {
        public string Locale => CultureInfo.InstalledUICulture.Name;
    }
}