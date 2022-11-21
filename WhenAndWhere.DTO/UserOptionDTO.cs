using System;
using System.ComponentModel.DataAnnotations.Schema;
using WhenAndWhere.DAL.Models;

namespace WhenAndWhere.DTO;

public class UserOptionDTO
{
    public UserDTO User { get; set; }
    public OptionDTO Option { get; set; }
    public DateTime TimeVoted { get; set; }
}

