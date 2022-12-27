using WhenAndWhere.BL.Interfaces;

namespace WhenAndWhere.BL.DTOs;

public class UserRoleDTO : IDtoLink
{ 
    public int FirstId { get; set; }
    public int SecondId { get; set; }
}

