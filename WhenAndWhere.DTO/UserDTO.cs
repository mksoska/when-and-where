namespace WhenAndWhere.DTO;

public class UserDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public byte[] Avatar { get; set; }
    public List<MeetupDTO> OwnedMeetups { get; set; }
    public List<OptionDTO> CreatedOptions { get; set; }
}