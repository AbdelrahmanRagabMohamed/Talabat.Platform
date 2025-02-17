using System.Linq.Expressions;
using Talabat.Core.Entites;

namespace Talabat.Core.Specifications;
public interface ISpecefications<T> where T : BaseEntity
{
    // _dbContext.Products.Where(P => P.Id == id)
    //                    .OrederBy(P => P.Name)
    //                    .Skip(skip).Take(take)
    //                    .Include(P => P.ProductBrand).Include(P => P.ProductType)


    // Signature for Property for Where Condation => [Where(P => P.Id == id)]
    public Expression<Func<T, bool>> Criteria { get; set; }

    // Signature for Property for List Of Includes => [Include(P => P.ProductBrand).Include(P => P.ProductType)]
    public List<Expression<Func<T, object>>> Includes { get; set; }

    // Signature for Property for OrderBy [OrderBy(P => P.Name)]
    public Expression<Func<T, object>> OrderBy { get; set; }

    // Signature for Property for OrderByDescending [OrderByDesc(P => P.Name)]
    public Expression<Func<T, object>> OrderByDescending { get; set; }

    // Signature for Property for SKIP
    public int Skip { get; set; }

    // Signature for Property for Take
    public int Take { get; set; }

    // Signature for Property for IsPaginationEnabled => And it will be True always if Pagination is Enabled [As Buisness Deal]
    public bool IsPaginationEnabled { get; set; }


}
