using Boilerplate.App.Bootstrap;
using Boilerplate.App.Services.General.Contracts;
using Xamarin.Forms;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System;

namespace Boilerplate.App
{
    public partial class App : Application
    {
        private readonly INavigationService _navigationSvc;

        public App()
        {
            InitializeComponent();

            AppContainer.RegisterDependencies();

            _navigationSvc = AppContainer.Resolve<INavigationService>();

            _ = InitializeNavigationAsync();
        }

        private async Task InitializeNavigationAsync()
        {
            await _navigationSvc.InitializeAsync();
        }

        protected override void OnStart()
        {
            base.OnStart();
            // OnStart logic here
        }

        protected override void OnSleep()
        {
            base.OnSleep();
            // OnSleep logic here
        }

        protected override void OnResume()
        {
            base.OnResume();
            // OnResume logic here
        }

        /// <summary>
        /// Opens Google Play deeplink to update the App
        /// </summary>
        private async Task OpenGooglePlayAsync()
        {
            var supportsMarket = await Launcher.CanOpenAsync("market://");
            if (supportsMarket)
            {
                await Launcher.TryOpenAsync(new Uri($"market://details?id={AppInfo.PackageName}"));
            }
            else
            {
                await Launcher.TryOpenAsync(new Uri($"http://play.google.com/store/apps/details?id={AppInfo.PackageName}"));
            }
        }
    }
}
