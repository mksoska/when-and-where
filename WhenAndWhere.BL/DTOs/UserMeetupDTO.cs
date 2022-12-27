using WhenAndWhere.BL.Interfaces;
using WhenAndWhere.DAL.Enums;

namespace WhenAndWhere.BL.DTOs;

public class UserMeetupDTO : IDtoLink
{
	public int FirstId { get; set; }
	public int SecondId { get; set; }
    public StateEnum State { get; set; }
    public DateTime DateInvited { get; set; }
}

