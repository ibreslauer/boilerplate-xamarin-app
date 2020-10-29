using Boilerplate.App.Bootstrap;
using Boilerplate.App.ViewModels.Base;
using Boilerplate.App.ViewModels.Samples;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Boilerplate.App.ViewModels
{
    public class MainMenuViewModel : ViewModelBase
    {
        public MainMenuViewModel() : base()
        {
            Title = "Boilerplate App";
        }

        public ICommand ModelessCommand => new Command(async () => await OnOpenModeless());
        public ICommand ModalCommand => 
            new Command(async param => await OnOpenModalPage(bool.TryParse((string)param, out bool showBar) && showBar));
        public ICommand PopupCommand => new Command(async () => await OnOpenPopup());
        public ICommand LookupCommand => new Command(async () => await OnOpenLookup());
        
        private Task OnOpenModeless()
        {
            return NavigationService.NavigateToAsync<SampleViewModel>();
        }

        private Task OnOpenModalPage(bool showNavBar = false)
        {
            return NavigationService.NavigateToModalAsync<SampleViewModel>(showNavBar: showNavBar);
        }

        private Task OnOpenPopup()
        {
            return NavigationService.NavigateToPopupAsync<SamplePopupViewModel>();
        }

        private async Task OnOpenLookup()
        {
            var returnValue = await ShowLookupPageAsync<SampleLookupViewModel, string>();
            if (!string.IsNullOrWhiteSpace(returnValue))
                await DialogService.ShowDialog($"Returned value: {returnValue}", Title);
        }
    }
}
