namespace Talabat.Core.Specifications;
public class ProductSpecParams
{
    public string? Sort { get; set; }
    public int? BrandId { get; set; }
    public int? TypeId { get; set; }

    private int pageSize = 5;  // Default Value if not set

    public int PageSize
    {
        get { return pageSize; }
        set { pageSize = value > 10 ? 10 : value; }
    }

    public int PageIndex { get; set; } = 1;   // Default Value if not set

    private string? search;

    public string? Search
    {
        get { return search; }
        set { search = value.ToLower(); }
    }


}
