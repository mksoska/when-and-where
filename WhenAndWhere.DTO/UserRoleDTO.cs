using System;
using System.ComponentModel.DataAnnotations.Schema;
using WhenAndWhere.DAL.Models;

namespace WhenAndWhere.DTO;

public class UserRoleDTO
{ 
    public UserDTO User { get; set; }
    public RoleDTO Role { get; set; }
}

