using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entites;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository;
public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{


    private readonly StoreContext _dbContext;

    // Ask CLR To Create object from DbContext (StoreContext)  => To Deal with DB
    public GenericRepository(StoreContext dbContext)
    {
        _dbContext = dbContext;
    }

    #region Without Specifications

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        if (typeof(T) == (typeof(Product)))     // حل مؤقت      
            return (IEnumerable<T>)await _dbContext.Products.Include(P => P.ProductBrand).Include(P => P.ProductType).ToListAsync();
        else
            return await _dbContext.Set<T>().ToListAsync();
    }


    public async Task<T> GetByIdAsync(int id)
        => await _dbContext.Set<T>().FindAsync(id);

    // return await _dbContext.Set<T>().Where(P => P.Id == id).Include(P => P.ProductBrand).Include(P => P.ProductType);

    #endregion


    #region With Specifications

    public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecefications<T> Spec)
    {
        //return await SpecificationsEvalutor<T>.GetQuery(_dbContext.Set<T>(), Spec).ToListAsync();
        return await ApplySpecifications(Spec).ToListAsync();
    }

    public async Task<T> GetByIdWithSpecAsync(ISpecefications<T> Spec)
    {
       //return await SpecificationsEvalutor<T>.GetQuery(_dbContext.Set<T>(), Spec).FirstOrDefaultAsync();
        return await ApplySpecifications(Spec).FirstOrDefaultAsync();
    }

    private IQueryable<T> ApplySpecifications(ISpecefications<T> Spec)      // تجنب التكرار
    {
        return SpecificationsEvalutor<T>.GetQuery(_dbContext.Set<T>(), Spec);
    }

    #endregion

}
