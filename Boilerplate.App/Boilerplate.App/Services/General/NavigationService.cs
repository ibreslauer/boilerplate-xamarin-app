using Boilerplate.Common.Constants;
using Boilerplate.App.Bootstrap;
using Boilerplate.App.Services.Data.Contracts;
using Boilerplate.App.Services.General.Contracts;
using Boilerplate.App.ViewModels;
using Boilerplate.App.ViewModels.Base;
using Boilerplate.App.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection;
using System.Globalization;
using Xamarin.Forms;
using Boilerplate.App.Views.Base;

namespace Boilerplate.App.Services.General
{
    public class NavigationService : INavigationService
    {
        private readonly IAuthenticationService _authenticationService;
        
        protected Application CurrentApplication => Application.Current;

        private INavigation Navigation
        {
            get
            {
                INavigation navigation;
                if (CurrentApplication.MainPage is AppRootPage mainPage)
                {
                    navigation = (mainPage.Detail as NavigationPage).CurrentPage.Navigation;
                }
                else
                {
                    navigation = CurrentApplication.MainPage is NavigationPage navPage ?
                        navPage.CurrentPage.Navigation :
                        CurrentApplication.MainPage.Navigation;
                }
                return navigation;
            }
        }

        public Page TopPage
        {
            get
            {
                if (Navigation.ModalStack?.Count > 0)
                {
                    return Navigation.ModalStack.Last();
                }
                return Navigation.NavigationStack.Last();
            }
        }

        public NavigationService(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        #region Navigation methods
        public async Task InitializeAsync()
        {
            if (_authenticationService.IsUserAuthenticated)
            {
                await NavigateToAsync<AppRootViewModel>();
            }
            else
            {
                await NavigateToAsync<LoginViewModel>();
            }
        }

        public Task NavigateToAsync<TViewModel>(object parameter = null) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task NavigateToAsync(Type viewModelType)
        {
            return InternalNavigateToAsync(viewModelType, null);
        }

        public Task InsertPageBeforeAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter, insertPageBefore: true);
        }

        public async Task NavigateToModalAsync<TViewModel>(object parameter = null, bool showNavBar = false) where TViewModel : ViewModelBase
        {
            Page page = CreateAndBindPage(typeof(TViewModel), PageNavigationMode.Modal);
            var vm = page.BindingContext as ViewModelBase;
            if (showNavBar)
            {
                page = new NavigationPage(page);
                AddCustomPageEventHandlers(page as NavigationPage, (page as NavigationPage).CurrentPage);
            }
            await Navigation.PushModalAsync(page);
            await vm?.InitializeAsync(parameter);
        }

        public async Task NavigateToModalAsync(Type viewModelType, object parameter = null, bool showNavBar = false)
        {
            Page page = CreateAndBindPage(viewModelType, PageNavigationMode.Modal);
            var vm = page.BindingContext as ViewModelBase;
            if (showNavBar)
            {
                page = new NavigationPage(page);
                AddCustomPageEventHandlers(page as NavigationPage, (page as NavigationPage).CurrentPage);
            }
            await Navigation.PushModalAsync(page);
            await vm?.InitializeAsync(parameter);
        }

        public async Task NavigateToModalAsync<TViewModel>(TViewModel viewModel, object parameter = null, bool showNavBar = false) where TViewModel : ViewModelBase
        {
            Page page = CreateAndBindPageWithViewModel(viewModel, PageNavigationMode.Modal);
            if (showNavBar)
            {
                page = new NavigationPage(page);
                AddCustomPageEventHandlers(page as NavigationPage, (page as NavigationPage).CurrentPage);
            }
            await Navigation.PushModalAsync(page);
            await viewModel.InitializeAsync(parameter);
        }

        public async Task NavigateToPopupAsync<TViewModel>(object parameter = null) where TViewModel : ViewModelBase
        {
            Page page = CreateAndBindPage(typeof(TViewModel), PageNavigationMode.Popup);
            var vm = page.BindingContext as ViewModelBase;
            if (page is BasePopupPage popupPage)
            {
                await Navigation.PushPopupAsync(popupPage);
                await vm?.InitializeAsync(parameter);
            }
            else
            {
                throw new InvalidOperationException($"{page.GetType().Name} is not of type PopupPage.");
            }
        }
        
        public async Task NavigateToPopupAsync(Type viewModelType, object parameter = null)
        {
            Page page = CreateAndBindPage(viewModelType, PageNavigationMode.Popup);
            var vm = page.BindingContext as ViewModelBase;
            if (page is BasePopupPage popupPage)
            {
                await Navigation.PushPopupAsync(popupPage);
                await vm?.InitializeAsync(parameter);
            }
            else
            {
                throw new InvalidOperationException($"{page.GetType().Name} is not of type PopupPage.");
            }
        }

        public async Task NavigateBackPopupAsync()
        {
            await Navigation.PopPopupAsync();
            if (TopPage is NavigationPage navPage)
            {
                if (navPage.RootPage.BindingContext is ViewModelBase vm)
                {
                    // this was necessary because OnAppearing doesn't get invoked when closing a popup
                    vm.OnAppearing();
                }
            }
        }

        public Task NavigateBackModalAsync()
        {
            return Navigation.PopModalAsync();
        }

        public async Task ClearBackStackToLogin()
        {
            await PopToRootAsync();
            await NavigateToAsync<LoginViewModel>();
        }

        public async Task NavigateBackAsync()
        {
            if (CurrentApplication.MainPage is AppRootPage mainPage)
            {
                await mainPage.Detail.Navigation.PopAsync();
            }
            else if (CurrentApplication.MainPage is NavigationPage navPage)
            {
                await navPage.Navigation.PopAsync();
            }
        }

        public async Task PopToRootAsync()
        {
            if (CurrentApplication.MainPage is AppRootPage mainPage)
            {
                await mainPage.Detail.Navigation.PopToRootAsync();
            }
            else if (CurrentApplication.MainPage is NavigationPage navPage)
            {
                await navPage.Navigation.PopToRootAsync();
            }
        }

        protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter, ViewModelBase viewModel = null, bool insertPageBefore = false)
        {
            Page page = viewModel == null ?
                CreateAndBindPage(viewModelType) :
                CreateAndBindPageWithViewModel(viewModel);

            if (page is LoginPage)
            {
                CurrentApplication.MainPage = new NavigationPage(page);
            }
            else if (page is AppRootPage)
            {
                CurrentApplication.MainPage = page;
            }
            else if (CurrentApplication.MainPage is AppRootPage mainPage)
            {
                if (mainPage.Detail is NavigationPage navigationPage)
                {
                    var currentPage = navigationPage?.CurrentPage;

                    if (currentPage == null || 
                        currentPage.GetType() != page.GetType())
                    {
                        AddCustomPageEventHandlers(navigationPage, page);
                        if (insertPageBefore)
                        {
                            navigationPage.Navigation.InsertPageBefore(page, currentPage);
                            await navigationPage.PopAsync();
                        }
                        else
                        {
                            await navigationPage.PushAsync(page);
                        }
                    }
                }
                else
                {
                    navigationPage = new NavigationPage(page);
                    mainPage.Detail = navigationPage;
                }

                mainPage.IsPresented = false;
            }
            else
            {
                if (CurrentApplication.MainPage is NavigationPage navPage)
                {
                    await navPage.PushAsync(page);
                }
                else
                {
                    CurrentApplication.MainPage = new NavigationPage(page);
                }
            }

            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
        }
        #endregion

        #region Helper methods
        protected Type GetPageTypeForViewModel(Type viewModelType)
        {
            var pageName = viewModelType.FullName
                .Replace("ViewModels", "Views") // namespace
                .Replace("ViewModel", "Page"); // name
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", pageName, viewModelAssemblyName);

            var viewType = Type.GetType(viewAssemblyName);
            _ = viewType ?? throw new KeyNotFoundException($"Page {pageName} was not found in {viewModelAssemblyName}");

            return viewType;
        }

        protected Page CreateAndBindPage(Type viewModelType, PageNavigationMode navMode = PageNavigationMode.Modeless)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new Exception($"Mapping type for {viewModelType} is not a page");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            ViewModelBase viewModel = AppContainer.Resolve(viewModelType) as ViewModelBase;
            viewModel.PageNavigationMode = navMode;
            page.BindingContext = viewModel;

            return page;
        }

        protected Page CreateAndBindPageWithViewModel<T>(T viewModel, PageNavigationMode navMode = PageNavigationMode.Modeless)
            where T : ViewModelBase
        {
            Type pageType = GetPageTypeForViewModel(viewModel.GetType());

            if (pageType == null)
            {
                throw new Exception($"Mapping type for {typeof(T)} is not a page");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            page.BindingContext = viewModel;

            viewModel.PageNavigationMode = navMode;

            return page;
        }

        private void AddCustomPageEventHandlers(NavigationPage navPage, Page contentPage)
        {
            if (navPage == null || contentPage == null) return;

            if (contentPage.BindingContext is ViewModelBase vmBase)
            {
                // Navigation Bar Background Color
                if (vmBase.OnBarBackgroundColorChange == null)
                    vmBase.OnBarBackgroundColorChange += (color) => navPage.BarBackgroundColor = color;
            }
        }

        public bool IsPageOnTop(Type viewModelType)
        {
            var pageType = GetPageTypeForViewModel(viewModelType);

            if (TopPage is NavigationPage navPage)
            {
                return navPage.RootPage.GetType().Equals(pageType);
            }

            return TopPage.GetType().Equals(pageType);
        }
        #endregion
    }
}
