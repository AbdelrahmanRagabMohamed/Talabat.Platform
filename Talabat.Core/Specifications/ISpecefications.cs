using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Specifications;
public interface ISpecefications<T> where T : BaseEntity
{
    // _dbContext.Products.Where(P => P.Id == id).Include(P => P.ProductBrand).Include(P => P.ProductType)

                                                                                           

    // Signature for Property for Where Condation => [Where(P => P.Id == id)]
    public Expression<Func<T, bool>> Criteria { get; set; }


    // Signature for Property for List Of Includes => [Include(P => P.ProductBrand).Include(P => P.ProductType)]
    public List<Expression<Func<T, object>>> Includes { get; set; }

}
