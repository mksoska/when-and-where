﻿using WhenAndWhere.DAL.Enums;

namespace WhenAndWhere.BL.DTOs;

public class RoleDTO
{
    public int Id { get; set; }
    public int MeetupId { get; set; }
    public string Name { get; set; }
}