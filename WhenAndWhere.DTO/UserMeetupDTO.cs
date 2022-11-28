using System;
using WhenAndWhere.DAL.Enums;
using WhenAndWhere.DAL.Models;

namespace WhenAndWhere.DTO;

public class UserMeetupDTO
{
	public int UserId { get; set; }
	public int MeetupId { get; set; }
    public StateEnum State { get; set; }
    public DateTime DateInvited { get; set; }
}

