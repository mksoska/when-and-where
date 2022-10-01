using System.ComponentModel.DataAnnotations;

namespace PizzaShopDAL.Models;

public class Admin : User
{
    [Key]
    public int Id { get; set; }
}