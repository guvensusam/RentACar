namespace RentACar.DTOs;

public class PagedResponse<T>
{
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }

    public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
}
