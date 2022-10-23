using System.ComponentModel.DataAnnotations.Schema;

namespace WhenAndWhere.DAL.Models;

public class UserOption : IEntityLink
{
    public int FirstId { get; set; }
    
    [ForeignKey(nameof(FirstId))]
    public virtual User User { get; set; }
    
    public int SecondId { get; set; }
    
    [ForeignKey(nameof(SecondId))]
    public virtual Option Option { get; set; }
    
    public DateTime TimeVoted { get; set; }
}