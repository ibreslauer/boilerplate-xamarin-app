using Boilerplate.Common.Models;
using System.Threading.Tasks;

namespace Boilerplate.App.Services.Data.Contracts
{
    public interface IAuthenticationService
    {
        Task<AuthResult> AuthenticateAsync(string username, string password);
        Task LogoutAsync();
        bool IsUserAuthenticated { get; }
    }
}
