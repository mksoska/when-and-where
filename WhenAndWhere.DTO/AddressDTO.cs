using WhenAndWhere.DAL.Models;

namespace WhenAndWhere.DTO;

public class AddressDTO
{ 
    public int Id { get; set; }
    public int OptionId { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string ZipCode { get; set; }
}