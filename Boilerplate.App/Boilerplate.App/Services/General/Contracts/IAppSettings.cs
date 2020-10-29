using Boilerplate.Common.Models;

namespace Boilerplate.App.Services.General.Contracts
{
    public interface IAppSettings
    {
        UserModel DeviceUser { get; set; }
        string CompanyName { get; set; }
        string ServerUrlSetting { get; set; }
        string AccessToken { get; set; }
        
        void AddItem(string key, string value);
        string GetItem(string key);
        void AddItem<T>(string key, T value);
        T GetItem<T>(string key);
    }
}
