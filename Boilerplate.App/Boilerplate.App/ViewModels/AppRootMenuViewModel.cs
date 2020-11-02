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

        public string WelcomeText => $"Welcome, {AppSettings.CurrentUser?.Username}!";

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

        private Task OnMenuItemTapped(object menuItemTappedEventArgs)
        {
            var menuItem = ((menuItemTappedEventArgs as ItemTappedEventArgs)?.Item as AppRootMenuItem);

            if (menuItem.MenuItemType == MenuItemType.Logout)
            {
                return RunAsBusyAsync(async () =>
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

            var type = menuItem.ViewModelToLoad;
            if (type == null) return Task.CompletedTask;

            switch (menuItem.NavigationMode)
            {
                case PageNavigationMode.Modeless:
                    return RunAsBusyAsync(async () => await NavigationService.NavigateToAsync(type));
                case PageNavigationMode.Modal:
                    return RunAsBusyAsync(async () => await NavigationService.NavigateToModalAsync(type));
                case PageNavigationMode.Popup:
                    return RunAsBusyAsync(async () => await NavigationService.NavigateToPopupAsync(type));
                default:
                    return Task.CompletedTask;
            }
        }

        private void LoadMenuItems()
        {
            MenuItems.Add(new AppRootMenuItem
            {
                MenuText = "About this app",
                ViewModelToLoad = typeof(AboutViewModel),
                NavigationMode = PageNavigationMode.Popup,
                MenuItemType = MenuItemType.Page,
                HasSeparator = true
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
