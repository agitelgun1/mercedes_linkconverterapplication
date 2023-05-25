using System.Threading.Tasks;

namespace LinkConverterApplication.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<bool> InsertAsync(T entity);
        Task<bool> DisableDataAsync(T entity);
        Task<T> GetAsync(T entity);
    }
}