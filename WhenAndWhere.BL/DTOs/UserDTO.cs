﻿using WhenAndWhere.BL.Interfaces;

namespace WhenAndWhere.BL.DTOs;

public class UserDTO : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    
    public string UserName { get; set; }
    
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public byte[] Avatar { get; set; }
}