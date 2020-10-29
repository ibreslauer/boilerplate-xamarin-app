using System.Threading.Tasks;
using Boilerplate.Common.Constants;
using Boilerplate.App.Services.Data.Base;
using Boilerplate.App.Services.Data.Contracts;

namespace Boilerplate.App.Services.Data
{
    public class AvailabilityService : BaseService, IAvailabilityService
    {
        public async Task<string> PingAsync()
        {
            string uri = GetUri(ApiConstants.PingEndpoint);
            return await GenericRepository.GetAsync<string>(uri);
        }
    }
}
