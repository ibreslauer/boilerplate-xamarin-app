using System.Threading.Tasks;
using Boilerplate.App.ViewModels.Base;

namespace Boilerplate.App.ViewModels
{
    public class AppRootViewModel : ViewModelBase
    {
        private AppRootMenuViewModel _appRootMenuViewModel;

        public AppRootViewModel(AppRootMenuViewModel appRootMenuViewModel)
            : base()
        {
            _appRootMenuViewModel = appRootMenuViewModel;
        }

        public AppRootMenuViewModel MenuViewModel
        {
            get => _appRootMenuViewModel;
            set => SetProperty(ref _appRootMenuViewModel, value);
        }

        public override async Task InitializeAsync(object data)
        {
            await Task.WhenAll
            (
                _appRootMenuViewModel.InitializeAsync(data),
                NavigationService.NavigateToAsync<MainMenuViewModel>()
            );
        }
    }
}
