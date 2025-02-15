using Talabat.Core.Entites;

namespace Talabat.Core.Specifications;
public class ProducWithBrandAndTypeSpecifications : BaseSpecifications<Product>
{

    // CTOR To Get All Products
    public ProducWithBrandAndTypeSpecifications(string Sort, int? BrandId, int? TypeId)
        : base(
            // Filteration
            (P =>
            (!BrandId.HasValue || P.ProductBrandId == BrandId)
            &&
            (!TypeId.HasValue || P.ProductTypeId == TypeId)
            )
            )
    {
        Includes.Add(P => P.ProductBrand);
        Includes.Add(P => P.ProductType);

        if (!string.IsNullOrEmpty(Sort))
        {
            switch (Sort)
            {
                case "PriceAsc":
                    AddOrderBy(P => P.Price);
                    break;
                case "PriceDesc":
                    AddOrderByDescending(P => P.Price);
                    break;
                default:
                    AddOrderBy(P => P.Name);
                    break;

            }

        }
    }

    // CTOR To Get Product By Id
    public ProducWithBrandAndTypeSpecifications(int id) : base(P => P.Id == id)
    {
        Includes.Add(P => P.ProductBrand);
        Includes.Add(P => P.ProductType);
    }


}
