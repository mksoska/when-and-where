namespace WhenAndWhere.DTO.Filter;

public class UserNameFilterDTO
{
    public string Name { get; set; }
    public int? RequestedPageNumber { get; set; }
    public int PageSize { get; set; }
    public string SortCriteria { get; set; }
    public bool SortAscending { get; set; }
}