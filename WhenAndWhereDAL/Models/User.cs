namespace PizzaShopDAL.Models;

public class User
{
    public int Id { get; set; }
    public String Name { get; set; }
    public String Surname { get; set; }
    public String Email { get; set; }
    public String PhoneNumber { get; set; }
    public byte[] Avatar { get; set; }
    public List<Meetup> CreatedMeetups { get; set; }
    public List<UserOption> UserOptions { get; set; }
    public List<Option> Options { get; set; }
}