using Talabat.Core.Entites;

namespace Talabat.Core.Specifications;
public class ProductWithFilterationForCountAsync : BaseSpecifications<Product>
{
    public ProductWithFilterationForCountAsync(ProductSpecParams Params)
        : base(
            // Filteration (Criteria)
            (P =>
            (string.IsNullOrEmpty(Params.Search) || P.Name.Contains(Params.Search)) // Search By Name
            &&
            (!Params.BrandId.HasValue || P.ProductBrandId == Params.BrandId)
            &&
            (!Params.TypeId.HasValue || P.ProductTypeId == Params.TypeId)
            )
            )
    {

    }
}
