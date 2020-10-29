using Boilerplate.App.ViewModels;
using Boilerplate.App.ViewModels.Base;
using System;
using System.Threading.Tasks;

namespace Boilerplate.App.Services.General.Contracts
{
    public interface INavigationService
    {
        Task InitializeAsync();
        Task NavigateToAsync<TViewModel>(object parameter = null) where TViewModel : ViewModelBase;
        Task NavigateToAsync(Type viewModelType);
        Task InsertPageBeforeAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;
        Task NavigateToModalAsync<TViewModel>(object parameter = null, bool showNavBar = false) where TViewModel : ViewModelBase;
        Task NavigateToModalAsync<TViewModel>(TViewModel viewModel, object parameter = null, bool showNavBar = false) where TViewModel : ViewModelBase;
        Task NavigateToPopupAsync<TViewModel>(object parameter = null) where TViewModel : ViewModelBase;
        Task NavigateBackPopupAsync();
        Task ClearBackStackToLogin();
        Task NavigateBackModalAsync();
        Task NavigateBackAsync();
        Task PopToRootAsync();
        bool IsPageOnTop(Type viewModelType);
    }
}
