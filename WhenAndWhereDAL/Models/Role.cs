using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WhenAndWhereDAL.Enums;

namespace WhenAndWhereDAL.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        public RoleEnum RoleName { get; set; }

        public virtual List<UserRole> AssignedUsers { get; set; }
    }
}

