using WhenAndWhere.BL.Interfaces;
using WhenAndWhere.DAL.Enums;

namespace WhenAndWhere.BL.DTOs;

public class MeetupDTO : IDto
{
    public int Id { get; set; }
    public int OwnerId { get; set; }
    public string Name { get; set; }
    public DateTime OptionsFrom { get; set; }
    public DateTime OptionsTo { get; set; }
    public byte[] Logo { get; set; }
    public MeetupType Type { get; set; }
}