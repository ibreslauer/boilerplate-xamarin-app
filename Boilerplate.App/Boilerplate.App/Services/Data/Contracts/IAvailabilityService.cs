using System.Threading.Tasks;

namespace Boilerplate.App.Services.Data.Contracts
{
    public interface IAvailabilityService
    {
        Task<string> PingAsync();
    }
}
