using System.Threading.Tasks;

namespace Boilerplate.Common.Contracts
{
    public interface ILookup<T>
    {
        Task<T> GetValue();
    }
}
