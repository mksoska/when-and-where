namespace WhenAndWhere.BL.Filter;

public class QueryFilterDto<TDto>
{
    //WhereValues
    public TDto Values { get; set; }
    public List<string> WhereColumns { get; set; }
    public int? RequestedPageNumber { get; set; }
    public int PageSize { get; set; }
    public string SortCriteria { get; set; }
    public bool SortAscending { get; set; }
    public string[] SelectColumns { get; set; }
}