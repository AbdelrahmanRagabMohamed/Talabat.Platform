using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entites;
using Talabat.Core.Specifications;

namespace Talabat.Repository;
public static class SpecificationsEvalutor<T> where T : BaseEntity
{

    // _dbContext.Set<T>().Where(P => P.Id == id).Include(P => P.ProductBrand).Include(P => P.ProductType);


    // Function To Build Query Dynamic
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecefications<T> Spec)
    {
        var Query = inputQuery; // _dbContext.Set<T>()


        if (Spec.Criteria is not null)
        {
            Query = Query.Where(Spec.Criteria);   // _dbContext.Set<T>().Where(P => P.Id == id)  Criteria   لو في   
        }

        if (Spec.OrderBy is not null)  // P => P.Name
        {
            Query = Query.OrderBy(Spec.OrderBy); //_dbContext.Products.OrederBy(P => P.Name)
        }

        if (Spec.OrderByDescending is not null)
        {
            Query = Query.OrderByDescending(Spec.OrderByDescending);  //_dbContext.Products.OrderByDescending(P => P.Name)
        }

        if (Spec.IsPaginationEnabled)
        {
            Query = Query.Skip(Spec.Skip).Take(Spec.Take);  // _dbContext.Products.Skip(PageSize * (PageIndex - 1)).Take(PageSize)
        }


        Query = Spec.Includes.Aggregate(Query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));
        // First Aggergate => _dbContext.Set<T>().Where(P => P.Id == id).Include(P => P.ProductBrand)
        // Second Aggergate => _dbContext.Set<T>().Where(P => P.Id == id).Include(P => P.ProductBrand).Include(P => P.ProductType)

        return Query;

    }
}
