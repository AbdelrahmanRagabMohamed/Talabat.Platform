namespace Talabat.Core.Entites;

public class ProductType : BaseEntity
{
    public string Name { get; set; }

    /// public ICollection<Product> Products { get; set; } => مش محتاجها في البيزنس
    /// صح و متفهمش انها 1 - 1 Relation تفهم ال  EF عشان ال Fluent Api الخاصة بها ب configurations هنعملها ال 
}
