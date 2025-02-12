using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Query = Query.Where(Spec.Criteria);   // _dbContext.Set<T>().Where(P => P.Id == id)
        }

        Query = Spec.Includes.Aggregate(Query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));
        // First Aggergate => _dbContext.Set<T>().Where(P => P.Id == id).Include(P => P.ProductBrand)
        // Second Aggergate => _dbContext.Set<T>().Where(P => P.Id == id).Include(P => P.ProductBrand).Include(P => P.ProductType)

        return Query;

    }
}
