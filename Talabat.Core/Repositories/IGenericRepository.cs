using Talabat.Core.Entites;

namespace Talabat.Core.Repositories;
public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync();

    Task<T> GetByIdAsync(int id);
}
