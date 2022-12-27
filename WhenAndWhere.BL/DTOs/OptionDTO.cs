using WhenAndWhere.BL.Interfaces;

namespace WhenAndWhere.BL.DTOs;

public class OptionDTO : IDto
{
    public int Id { get; set; }
    public int MeetupId { get; set; }
    public int OwnerId { get; set; }
    public int AddressId { get; set; }
    public string Label { get; set; }
    public DateTime Time { get; set; }
}