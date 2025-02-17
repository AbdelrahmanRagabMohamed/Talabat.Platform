namespace Talabat.APIs.Helpers;

public class Pagination<T>
{
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
    public int Count { get; set; }
    public IReadOnlyList<T> Data { get; set; }

}
