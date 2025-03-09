using Talabat.Core.Entites;

namespace Talabat.Core.Specifications;
public class ProducWithBrandAndTypeSpecifications : BaseSpecifications<Product>
{

    // CTOR To Get All Products
    public ProducWithBrandAndTypeSpecifications(ProductSpecParams Params)
        : base(
            // Filteration
            (P =>
            (string.IsNullOrEmpty(Params.Search) || P.Name.Contains(Params.Search))   // Search By Name
            &&
            (!Params.BrandId.HasValue || P.ProductBrandId == Params.BrandId)
            &&
            (!Params.TypeId.HasValue || P.ProductTypeId == Params.TypeId)
            )
            )
    {
        Includes.Add(P => P.ProductBrand);
        Includes.Add(P => P.ProductType);

        // Sorting
        if (!string.IsNullOrEmpty(Params.Sort))
        {
            switch (Params.Sort)
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

        // Pagination
        /* If Products = 100

        if PageSize = 10
        if PageIndex = 5

         So , Skip = 40
              Take = 10

        Skip = PageSize * (PageIndex - 1)
        
         */

        ApplyPagination(Params.PageSize * (Params.PageIndex - 1), Params.PageSize);

    }

    // CTOR To Get Product By Id 
    public ProducWithBrandAndTypeSpecifications(int id) : base(P => P.Id == id)
    {
        Includes.Add(P => P.ProductBrand);
        Includes.Add(P => P.ProductType);
    }


}
