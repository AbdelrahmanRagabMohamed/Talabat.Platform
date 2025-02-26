using Talabat.Core.Entites;
using Talabat.Core.Repositories;

namespace Talabat.Core;
public interface IUnitOfWork : IAsyncDisposable
{
    Task<int> CompleteAsync();

    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;


}
