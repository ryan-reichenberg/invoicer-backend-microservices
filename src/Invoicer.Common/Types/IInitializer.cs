using System.Threading.Tasks;

namespace Invoicer.Common.Types
{
    public interface IInitializer
    {
        Task InitializeAsync();
    }
}