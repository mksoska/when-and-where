namespace WhenAndWhere.DTO;

public class OptionDTO
{
    public int Id { get; set; }
    public AddressDTO Meetup { get; set; }
    public UserProfileDTO User { get; set; }
    public AddressDTO Address { get; set; }
    public string Label { get; set; }
    public DateTime Time { get; set; }
}