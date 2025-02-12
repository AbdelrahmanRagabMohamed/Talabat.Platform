using Talabat.Core.Entites;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repositories;
public interface IGenericRepository<T> where T : BaseEntity
{

    #region Without Specifications 
    Task<IEnumerable<T>> GetAllAsync();

    Task<T> GetByIdAsync(int id);

    #endregion


    #region With Specifications 

    Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecefications<T> Spec);

    Task<T> GetByIdWithSpecAsync(ISpecefications<T> Spec);

    #endregion
}
