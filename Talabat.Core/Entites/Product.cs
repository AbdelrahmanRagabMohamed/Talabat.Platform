namespace Talabat.Core.Entites;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string PictureUrl { get; set; }
    public decimal Price { get; set; }

    /// [ForeignKey("ProductBrand")]  => Fk مش محتاجها عشان مغيرتش في اسم ال   
    public int ProductBrandId { get; set; } // FK

    /// [ForeignKey("ProductType")]  => Fk مش محتاجها عشان مغيرتش في اسم ال 
    public int ProductTypeId { get; set; } // FK

    /// Relations Between Product and ProductBrand Or ProductType is :  1 ==> Many
    public ProductBrand ProductBrand { get; set; }

    public ProductType ProductType { get; set; }

}
