using System;
using WhenAndWhere.DAL.Enums;
using WhenAndWhere.DAL.Models;

namespace WhenAndWhere.DTO;

public class UserMeetupDTO
{
	public UserDTO User { get; set; }
	public MeetupDTO Meetup { get; set; }
    public StateEnum State { get; set; }
    public DateTime DateInvited { get; set; }
}

