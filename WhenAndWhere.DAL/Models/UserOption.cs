using System.ComponentModel.DataAnnotations.Schema;

namespace WhenAndWhere.DAL.Models;

public class UserOption
{
    public int UserId { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; }
    
    public int OptionId { get; set; }
    
    [ForeignKey(nameof(OptionId))]
    public virtual Option Option { get; set; }
    
    public DateTime TimeVoted { get; set; }
}