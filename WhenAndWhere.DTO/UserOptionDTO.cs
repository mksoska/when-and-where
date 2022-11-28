using System;
using System.ComponentModel.DataAnnotations.Schema;
using WhenAndWhere.DAL.Models;

namespace WhenAndWhere.DTO;

public class UserOptionDTO
{
    public int UserId { get; set; }
    public int OptionId { get; set; }
    public DateTime TimeVoted { get; set; }
}

