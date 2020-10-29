using Acr.UserDialogs.Infrastructure;
using Boilerplate.App.Services.Data.Contracts;
using Boilerplate.App.ViewModels.Base;
using Boilerplate.App.ViewModels.Samples;
using Boilerplate.Common.Constants;
using Boilerplate.Common.Extensions;
using Boilerplate.Common.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Boilerplate.App.ViewModels
{
    public class AppRootMenuViewModel : ViewModelBase
    {
        private ObservableCollection<AppRootMenuItem> _menuItems;
        private readonly IAuthenticationService _authenticationService;

        public string WelcomeText => $"Welcome, {AppSettings.DeviceUser?.Email}!";

        public AppRootMenuViewModel(IAuthenticationService authenticationService)
            : base()
        {
            _authenticationService = authenticationService;
            MenuItems = new ObservableCollection<AppRootMenuItem>();

            LoadMenuItems();
        }

        public ICommand MenuItemTappedCommand => new Command(async (args) => await OnMenuItemTapped(args));

        public ObservableCollection<AppRootMenuItem> MenuItems
        {
            get => _menuItems;
            set => SetProperty(ref _menuItems, value);
        }

        private async Task OnMenuItemTapped(object menuItemTappedEventArgs)
        {
            var menuItem = ((menuItemTappedEventArgs as ItemTappedEventArgs)?.Item as AppRootMenuItem);

            if (menuItem?.MenuItemType == MenuItemType.Logout)
            {
                await RunAsBusyAsync(async () =>
                {
                    await _authenticationService.LogoutAsync();                    
                    try
                    {
                        await NavigationService.ClearBackStackToLogin();
                    }
                    catch (Exception ex)
                    {
                        Log.Error(GetType().Name, ex.GetErrorMessage());
                    }
                });
            }

            var type = menuItem?.ViewModelToLoad;
            if (type != null)
            {
                await NavigationService.NavigateToAsync(type);
            }
        }

        private void LoadMenuItems()
        {
            MenuItems.Add(new AppRootMenuItem
            {
                MenuText = "Open Modeless Page",
                ViewModelToLoad = typeof(SampleViewModel),
                MenuItemType = MenuItemType.Page
            });

            MenuItems.Add(new AppRootMenuItem
            {
                MenuText = "Log out",
                ViewModelToLoad = typeof(LoginViewModel),
                MenuItemType = MenuItemType.Logout
            });
        }
    }
}
