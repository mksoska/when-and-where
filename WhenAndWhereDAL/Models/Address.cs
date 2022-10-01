using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaShopDAL.Models;

public class Address
{
    [Key]
    public int Id { get; set; }
    public int OptionId { get; set; }
    
    [ForeignKey(nameof(OptionId))]
    public Option Option { get; set; }
    public String State { get; set; }
    public String City { get; set; }
    public String Street { get; set; }
    public String Number { get; set; }
    public String ZipCode { get; set; }
}