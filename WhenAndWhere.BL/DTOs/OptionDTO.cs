using System.ComponentModel.DataAnnotations;
using WhenAndWhere.BL.DTOs.ValidationAttributes;
using WhenAndWhere.BL.Interfaces;

namespace WhenAndWhere.BL.DTOs;

public class OptionDTO : IDto
{
    public int Id { get; set; }
    public int MeetupId { get; set; }
    public int OwnerId { get; set; }
    [Required]
    [StringLength(64)]
    public string Label { get; set; }

    [StringLength(64)]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only for State field, please")]
    public string State { get; set; }
    [Required]
    [StringLength(64)]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only for City field, please")]
    public string City { get; set; }
    [StringLength(64)]
    public string Street { get; set; }
    [StringLength(64)]
    public string Number { get; set; }
    [StringLength(64)]
    public string ZipCode { get; set; }
    [Required]
    [DataType(DataType.Date)]
    [MyDate(EndDatePropertyName = "End", StartDatePropertyName = "Start", ErrorMessage = "End date must be after start date.")]
    public DateTime Start { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateTime End { get; set; }

}