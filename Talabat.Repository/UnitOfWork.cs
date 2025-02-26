using System.Collections;
using Talabat.Core;
using Talabat.Core.Entites;
using Talabat.Core.Repositories;
using Talabat.Repository.Data;

namespace Talabat.Repository;
public class UnitOfWork : IUnitOfWork
{

    private Hashtable _repositories;  // Hashtable Property => To Store The Object If it is Created Before . 

    private readonly StoreContext _dbContext;

    public UnitOfWork(StoreContext dbContext)
    {
        _dbContext = dbContext;
        _repositories = new Hashtable();
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
        var type = typeof(TEntity).Name;

        if (!_repositories.ContainsKey(type))  // First Time (If Not Exsited)
        {
            var Repository = new GenericRepository<TEntity>(_dbContext);
            _repositories.Add(type, Repository);

            // return _repositories[type] as GenericRepository<TEntity>;    => لتجنب التكرار
        }

        //(If Exsited)
        return _repositories[type] as GenericRepository<TEntity>;
    }


    public async Task<int> CompleteAsync()
      => await _dbContext.SaveChangesAsync();

    public async ValueTask DisposeAsync()
        => await _dbContext.DisposeAsync();

}
