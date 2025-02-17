using Talabat.Core.Entites;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repositories;
public interface IGenericRepository<T> where T : BaseEntity
{

    #region Without Specifications 

    Task<IReadOnlyList<T>> GetAllAsync();

    Task<T> GetByIdAsync(int id);

    #endregion


    #region With Specifications 

    Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecefications<T> Spec);

    Task<T> GetByIdWithSpecAsync(ISpecefications<T> Spec);

    Task<int> GetCountWithSpecAsync(ISpecefications<T> Spec);
    #endregion
}
