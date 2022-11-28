using System;
using System.ComponentModel.DataAnnotations.Schema;
using WhenAndWhere.DAL.Models;

namespace WhenAndWhere.DTO;

public class UserRoleDTO
{ 
    public int UserId { get; set; }
    public int RoleId { get; set; }
}

