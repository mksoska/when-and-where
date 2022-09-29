using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaShopDAL.Models;

public class UserOption
{
    public int UserId { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
    
    public int OptionId { get; set; }
    
    [ForeignKey(nameof(OptionId))]
    public Option Option { get; set; }
    
    public DateTime TimeVoted { get; set; }
}