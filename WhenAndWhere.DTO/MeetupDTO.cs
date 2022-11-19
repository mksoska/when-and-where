using WhenAndWhere.DAL.Enums;

namespace WhenAndWhere.DTO;

public class MeetupDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime OptionsFrom { get; set; }
    public DateTime OptionsTo { get; set; }
    public byte[] Logo { get; set; }
    public MeetupType Type { get; set; }
    public UserDTO Owner { get; set; }
}