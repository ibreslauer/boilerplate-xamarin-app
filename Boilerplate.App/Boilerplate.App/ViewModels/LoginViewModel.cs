using Boilerplate.App.Contracts;
using Boilerplate.App.CrossPlatform;
using Boilerplate.App.Services.Data.Contracts;
using Boilerplate.App.ViewModels.Base;
using Boilerplate.Common.Constants;
using Boilerplate.Common.Validation;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Boilerplate.App.ViewModels
{
    public class LoginViewModel : ViewModelBase, ILookup<bool>
    {
        private readonly IAuthenticationService _authSvc;
        private readonly TaskCompletionSource<bool> _diagResult = new TaskCompletionSource<bool>();

        private ValidatableObject<string> _userName;
        public ValidatableObject<string> UserName
        {
            get => _userName;
            set
            {
                SetProperty(ref _userName, value);
                OnPropertyChanged(nameof(IsUsernameSet));
            }
        }

        private ValidatableObject<string> _password;
        public ValidatableObject<string> Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public bool IsUsernameSet => !string.IsNullOrWhiteSpace(UserName.Value);
        public string DeviceId => AppRuntime.DEVICE_ID.Value;
        public string AppVersion => $"v{AppRuntime.APP_VERSION.Value}";

        public LoginViewModel(IAuthenticationService authenticationService)
            : base()
        {
            Title = "Log in";
            _authSvc = authenticationService;

            _userName = new ValidatableObject<string>();
            UserName.Value = AppSettings.CurrentUser?.Email;

            _password = new ValidatableObject<string>();

            AddValidations();

            SubscribeToMessages();
        }

        private void SubscribeToMessages()
        {
            MessagingCenter.Subscribe<SettingsViewModel>(this, AppConstants.MSG_SETTINGS_SAVED, (sender) =>
            {
                UserName.Value = AppSettings.CurrentUser?.Email;
                OnPropertyChanged(nameof(IsUsernameSet));
            });
        }

        public ICommand ChangeUserCommand => new Command(OnChangeUser);
        public ICommand SettingsCommand => new Command(OnSettings);
        public ICommand LoginCommand => new Command(async () => await OnLogin());
        public ICommand SkipLoginCommand => new Command(async () =>
        {
            UserName.Value = Password.Value = "guest";
            await OnLogin();
        });

        private async void OnSettings()
        {
            await NavigationService.NavigateToAsync<SettingsViewModel>();
        }

        private void OnChangeUser()
        {
            UserName.Value = null;
            OnPropertyChanged(nameof(IsUsernameSet));
        }

        private async Task OnLogin()
        {
            if (IsBusy) return;
            if (!await CheckSettings()) return;
            if (!Validate())
            {
                DialogService.ShowToast(string.Join(Environment.NewLine, UserName.Errors.Union(Password.Errors)));
                return;
            }

            if (!ConnectionService.IsConnected)
            {
                await DialogService.ShowDialog("Check internet connectivity.", "Connection Error");
                return;
            }

            await RunAsBusyAsync(async () =>
            {
                var authResult = await _authSvc.AuthenticateAsync(UserName.Value, Password.Value);
                if (!authResult.Success)
                {
                    await DialogService.ShowDialog("Incorrect Username/Password.", Title);
                    ClearPage();
                    return;
                }

                AppSettings.CurrentUser = authResult.User;
                ClearPage();

                if (PageNavigationMode == PageNavigationMode.Modal)
                {
                    // resuming app after re-login
                    _diagResult.SetResult(true);
                }
                else
                {
                    // starting app
                    await NavigationService.NavigateToAsync<AppRootViewModel>();
                }
            });
        }

        protected override Task OnBack()
        {
            if (PageNavigationMode != PageNavigationMode.Modal)
                return base.OnBack();

            _diagResult.SetResult(false);
            return Task.CompletedTask;
        }

        public Task<bool> GetValue()
        {
            return _diagResult.Task;
        }

        private async Task<bool> CheckSettings()
        {
            if (string.IsNullOrEmpty(AppSettings.ServerUrlSetting))
            {
                await DialogService.ShowDialog("Configure App Settings before logging in.", Title);
                ClearPage();
                return false;
            }
            return true;
        }

        private void ClearPage()
        {
            UserName.Value = string.Empty;
            Password.Value = string.Empty;
        }

        private void AddValidations()
        {
            _userName.Validations.Add(new IsNotNullOrEmptyRule<string> { InvalidMessage = $"Enter your username." });
            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { InvalidMessage = $"Enter your password." });
        }

        private bool Validate()
        {
            return UserName.Validate() && Password.Validate();
        }
    }
}
