using WhenAndWhere.BL.Interfaces;

namespace WhenAndWhere.BL.DTOs;

public class RoleDTO : IDto
{
    public int Id { get; set; }
    public int MeetupId { get; set; }
    public string Name { get; set; }
}