using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhenAndWhere.DAL.Models;

public class UserRole : IEntityLink
{
    public int FirstId { get; set; }

    [ForeignKey(nameof(FirstId))]
    public virtual User User { get; set; }

    public int SecondId { get; set; }

    [ForeignKey(nameof(SecondId))]
    public virtual Role Role { get; set; }

}

