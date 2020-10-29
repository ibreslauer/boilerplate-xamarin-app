using Boilerplate.App.Bootstrap;
using Boilerplate.App.CrossPlatform;
using Boilerplate.Common.Base;
using Boilerplate.App.Services.General.Contracts;
using Boilerplate.Common.Constants;
using Boilerplate.Common.Extensions;
using Boilerplate.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Boilerplate.App.ViewModels.Base
{
    public partial class ViewModelBase : BindableBase, IDisposable
    {
        #region Fields & properties
        private readonly Lazy<IConnectionService> _connectionService = new Lazy<IConnectionService>(() => AppContainer.Resolve<IConnectionService>());
        private readonly Lazy<INavigationService> _navigationService = new Lazy<INavigationService>(() => AppContainer.Resolve<INavigationService>());
        private readonly Lazy<IDialogService> _dialogService = new Lazy<IDialogService>(() => AppContainer.Resolve<IDialogService>());
        private readonly Lazy<IAppSettings> _appSettings = new Lazy<IAppSettings>(() => AppContainer.Resolve<IAppSettings>());        

        protected IConnectionService ConnectionService => _connectionService.Value;
        protected INavigationService NavigationService => _navigationService.Value;
        protected IDialogService DialogService => _dialogService.Value;
        protected ISoundService Sounds { get; } = DependencyService.Get<ISoundService>();
        protected IAppSettings AppSettings => _appSettings.Value;
        protected bool IsActivePage => NavigationService.IsPageOnTop(this.GetType());
        public PageNavigationMode PageNavigationMode { get; set; } = PageNavigationMode.Modeless;

        protected string _title = string.Empty;
        public virtual string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public Action<Color> OnBarBackgroundColorChange;

        protected Color _barBackgroundColor;
        public virtual Color BarBackgroundColor
        {
            get => _barBackgroundColor;
            set
            {
                SetProperty(ref _barBackgroundColor, value);
                OnBarBackgroundColorChange?.Invoke(value);
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public bool BackNavigationEnabled { get; set; } = true;
        #endregion
        
        public ViewModelBase()
        {
            _barBackgroundColor = (Color)Application.Current.Resources["AppLightBlue"];
            InitializeNavBarColor();
        }

        private void InitializeNavBarColor()
        {
            OnBarBackgroundColorChange?.Invoke(BarBackgroundColor);
        }

        public virtual ICommand BackCommand => new Command(async () => await OnBack());
        public ICommand CloseAppCommand => new Command(OnCloseApp);

        protected virtual Task OnBack()
        {
            switch (PageNavigationMode)
            {
                case PageNavigationMode.Modeless:
                    return NavigationService.NavigateBackAsync();
                case PageNavigationMode.Modal:
                    return NavigationService.NavigateBackModalAsync();
                case PageNavigationMode.Popup:
                    return NavigationService.NavigateBackPopupAsync();
                default:
                    return Task.CompletedTask;
            }
        }

        private void OnCloseApp()
        {
            var closeService = DependencyService.Get<ICloseAppService>();
            closeService?.Close();
        }

        #region INotifyPropertyChanged
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
        #endregion

        /// <summary>
        /// Wrapper for longer-running actions that require user login and displaying 'loading spinner' (utilizes IsBusy flag for the activity indicator).
        /// </summary>
        protected async Task RunAsBusyAsync(Func<Task> action, string errorMsg = null)
        {
            if (IsBusy) return;

            IsBusy = true;
            try
            {
                await action();
            }
            catch (ApiAuthException)
            {
                await AttemptLoginAndRerunActionAsync(action);
            }
            catch (Exception ex)
            {
                await DialogService.ShowDialog(errorMsg != null
                    ? $"{errorMsg} {ex.GetErrorMessage()}"
                    : ex.GetErrorMessage(), Title);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task AttemptLoginAndRerunActionAsync(Func<Task> action)
        {
            DialogService.ShowToast("You've been logged out. Please log in again.");
            try
            {
                var loginSuccess = await ShowLookupPageAsync<LoginViewModel, bool>();
                if (loginSuccess)
                {
                    // retry action
                    await action();
                }
            }
            catch (Exception ex)
            {
                await DialogService.ShowDialog(ex.GetErrorMessage(), Title);
            }
        }

        #region Virtuals
        public virtual Task InitializeAsync(object data)
        {
            return Task.CompletedTask;
        }

        internal virtual bool OnBackButtonPressed()
        {
            return !BackNavigationEnabled;
        }

        internal virtual void OnAppearing()
        {
            // re-initialize the selected nav bar color in case it was changed on another Page
            InitializeNavBarColor();
        }

        internal virtual void OnDisappearing()
        {
            Dispose();
        }

        #endregion

        public virtual void Dispose()
        {
        }
    }
}
