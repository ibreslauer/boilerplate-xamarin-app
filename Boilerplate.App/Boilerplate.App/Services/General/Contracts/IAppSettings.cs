using Boilerplate.Common.Models;

namespace Boilerplate.App.Services.General.Contracts
{
    public interface IAppSettings
    {
        UserModel CurrentUser { get; set; }
        string CompanyName { get; set; }
        string ServerUrlSetting { get; set; }
        string AccessToken { get; set; }
    }
}
