using WhenAndWhere.BL.Interfaces;

namespace WhenAndWhere.BL.DTOs;

public class UserOptionDTO : IDtoLink
{
    public int FirstId { get; set; }
    public int SecondId { get; set; }
    public DateTime TimeVoted { get; set; }
}

