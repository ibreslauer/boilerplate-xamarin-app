using Boilerplate.App.Services.Data.Contracts;
using Boilerplate.App.ViewModels.Base;
using Boilerplate.Common.Constants;
using Boilerplate.Common.Extensions;
using Boilerplate.Common.Validation;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Boilerplate.App.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly IAvailabilityService _availabilitySvc;

        private ValidatableObject<string> _apiUrl;
        public ValidatableObject<string> ApiUrl
        {
            get => _apiUrl;
            set => SetProperty(ref _apiUrl, value);
        }

        public SettingsViewModel(IAvailabilityService availabilityService)
            : base()
        {
            _availabilitySvc = availabilityService;
            
            _apiUrl = new ValidatableObject<string> { Value = AppSettings.ServerUrlSetting.ToRelativeUrl() };
            AddValidations();
            
            Title = "App settings";
        }

        public ICommand SaveCommand => new Command(OnSave);
        public ICommand TestCommand => new Command(async () => await OnTest());

        private void OnSave()
        {
            if (!ApiUrl.Validate())
            {
                DialogService.ShowToast(string.Join(Environment.NewLine, ApiUrl.Errors));
                return;
            }
            
            AppSettings.ServerUrlSetting = ApiUrl.Value.ToAbsoluteUrl();
            MessagingCenter.Send(this, AppConstants.MSG_SETTINGS_SAVED);

            DialogService.ShowToast("Saved successfully.");
        }

        private Task OnTest()
        {
            return RunAsBusyAsync(async () =>
            {
                var pingResponse = await _availabilitySvc.PingAsync();
                if (!string.IsNullOrWhiteSpace(pingResponse))
                {
                    DialogService.ShowToast("Connection OK");
                }
            });
        }

        private void AddValidations()
        {
            _apiUrl.Validations.Add(new UrlRule<string> { InvalidMessage = "Provided URL is not valid." });
        }
    }
}
