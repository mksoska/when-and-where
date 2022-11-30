using WhenAndWhere.DAL.Enums;

namespace WhenAndWhere.BL.DTOs;

public class RoleDTO
{
    public int Id { get; set; }
    public int MeetupId { get; set; }
    public RoleEnum RoleName { get; set; }
}