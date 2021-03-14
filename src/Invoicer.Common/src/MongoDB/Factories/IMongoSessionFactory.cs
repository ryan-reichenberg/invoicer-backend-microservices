using System.Threading.Tasks;
using MongoDB.Driver;

namespace Invoicer.Common.MongoDB.Factories
{
    public interface IMongoSessionFactory
    {
        Task<IClientSessionHandle> CreateAsync();
    }
}