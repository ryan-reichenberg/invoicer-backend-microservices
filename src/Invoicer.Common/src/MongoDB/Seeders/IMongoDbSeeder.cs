using System.Threading.Tasks;
using MongoDB.Driver;

namespace Invoicer.Common.MongoDB.Seeders
{
    public interface IMongoDbSeeder
    {
        Task SeedAsync(IMongoDatabase database);
    }
}