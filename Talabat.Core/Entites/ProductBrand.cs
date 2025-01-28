using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entites;

public class ProductBrand : BaseEntity
{ 
    public string Name { get; set; }

    /// public ICollection<Product> Products { get; set; } => مش محتاجها في البيزنس
    /// صح و متفهمش انها 1 - 1 Relation تفهم ال  EF عشان ال Fluent Api الخاصة بها ب configurations هنعملها ال 
    
}
