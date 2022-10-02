using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhenAndWhereDAL.Models;

public class Address
{
    [Key]
    public int Id { get; set; }

    public int OptionId { get; set; }
    
    [ForeignKey(nameof(OptionId))]
    public virtual Option Option { get; set; }

    public string State { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string ZipCode { get; set; }
}