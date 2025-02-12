using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Specifications;
public class ProducWithBrandAndTypeSpecifications : BaseSpecifications<Product>
{

    // CTOR To Get All Products
    public ProducWithBrandAndTypeSpecifications() : base()
    {
        Includes.Add(P => P.ProductBrand);
        Includes.Add(P => P.ProductType);
    }

    // CTOR To Get Product By Id
    public ProducWithBrandAndTypeSpecifications(int id) : base(P => P.Id == id)
    {
        Includes.Add(P => P.ProductBrand);
        Includes.Add(P => P.ProductType);
    }


}
