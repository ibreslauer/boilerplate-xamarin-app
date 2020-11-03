using System.Threading.Tasks;

namespace Boilerplate.App.Contracts
{
    public interface ILookup<T>
    {
        Task<T> GetValue();
    }
}
