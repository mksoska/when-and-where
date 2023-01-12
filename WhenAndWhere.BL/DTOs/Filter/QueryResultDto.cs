namespace WhenAndWhere.BL.Filter;

public class QueryResultDto<TDto>
{
    public int TotalItemsCount { get; set; }
    public int? RequestedPageNumber { get; set; }
    public int PageSize { get; set; }
    public IEnumerable<TDto> Items { get; set; }
}