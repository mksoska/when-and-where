using WhenAndWhere.DAL.Enums;
using WhenAndWhere.DAL.Models;

namespace WhenAndWhere.DTO;

public class RoleDTO
{
    public int Id { get; set; }
    public int MeetupId { get; set; }
    public RoleEnum RoleName { get; set; }
}