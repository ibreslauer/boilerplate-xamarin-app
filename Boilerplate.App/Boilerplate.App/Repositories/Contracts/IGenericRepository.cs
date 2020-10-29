using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.App.Repositories.Contracts
{
    public interface IGenericRepository
    {
        Task<T> GetAsync<T>(string uri, CancellationToken? token = null);
        Task<T> PostAsync<T>(string uri, T data, CancellationToken? token = null);
        Task<T> PutAsync<T>(string uri, T data, CancellationToken? token = null);
        Task<TR> PutAsync<T, TR>(string uri, T data, CancellationToken? token = null);
        Task DeleteAsync(string uri, CancellationToken? token = null);
        Task<TR> DeleteAsync<TR>(string uri, CancellationToken? token = null);
        Task<TR> PostAsync<T, TR>(string uri, T data, CancellationToken? token = null);
    }
}
