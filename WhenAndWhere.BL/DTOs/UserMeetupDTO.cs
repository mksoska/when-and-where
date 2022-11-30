using WhenAndWhere.DAL.Enums;

namespace WhenAndWhere.BL.DTOs;

public class UserMeetupDTO
{
	public int UserId { get; set; }
	public int MeetupId { get; set; }
    public StateEnum State { get; set; }
    public DateTime DateInvited { get; set; }
}

