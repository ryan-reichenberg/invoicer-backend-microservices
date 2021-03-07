using Microsoft.EntityFrameworkCore;

namespace Invoicer.Common.DataAccess
{
    public interface IDbContext<T> where T: class
    {
        DbSet<T> DataSet { get; set; }
    }
}
