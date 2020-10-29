using Acr.UserDialogs.Infrastructure;
using Boilerplate.App.Services.Data.Base;
using Boilerplate.App.CrossPlatform;
using Boilerplate.Common.Constants;
using Boilerplate.Common.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using Boilerplate.App.Services.Data.Contracts;

namespace Boilerplate.App.Services.Data
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        public async Task<AuthResult> AuthenticateAsync(string username, string passwordHash)
        {
            string uri = GetUri(ApiConstants.AuthEndpoint);
            var authenticatingUser = new UserModel
            {
                Username = username,
                PasswordHash = passwordHash
            };
            
            var authResult = await GenericRepository
                .PostAsync<UserModel, AuthResult>(uri, authenticatingUser);
            // store token
            SettingsService.AccessToken = authResult.AccessToken;

            return authResult;
        }

        public async Task LogoutAsync()
        {
            string uri = GetUri(
                endpoint: ApiConstants.AuthEndpoint, 
                action: "deactivate");
            try
            {
                var response = await GenericRepository.PutAsync(uri,
                    new TokenModel
                    {
                        UserId = SettingsService.DeviceUser.Id,
                        DeviceId = AppRuntime.DEVICE_ID.Value,
                        Token = SettingsService.AccessToken
                    },
                    token: new CancellationTokenSource(4000).Token);

                if (!response.IsActive)
                {
                    SettingsService.AccessToken = null;
                }
            }
            catch (Exception ex)
            {
                if (SettingsService?.AccessToken != null)
                    SettingsService.AccessToken = null;
                
                Log.Error(GetType().Name, ex.Message);
            }
        }

        public bool IsUserAuthenticated => !string.IsNullOrWhiteSpace(SettingsService.AccessToken);
    }
}
