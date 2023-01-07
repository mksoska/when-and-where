using WhenAndWhere.BL.Interfaces;
using WhenAndWhere.DAL.Enums;
using System.ComponentModel.DataAnnotations;
using WhenAndWhere.BL.DTOs.ValidationAttributes;

namespace WhenAndWhere.BL.DTOs;

public class MeetupDTO : IDto
{
    public int Id { get; set; }
    public int OwnerId { get; set; }

    [Required]
    [StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 1)]
    public string Name { get; set; }
    
    [StringLength(200, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 0)]
    public string Description { get; set; }
    
    [Required]
    [DataType(DataType.Date)]
    [MyDate(EndDatePropertyName = "OptionsTo", StartDatePropertyName = "OptionsFrom", ErrorMessage = "OptionsTo must be after OptionsFrom.")]
    public DateTime OptionsFrom { get; set; }
    
    [Required]
    [DataType(DataType.Date)]
    public DateTime OptionsTo { get; set; }
    
    [Required]
    [DataType(DataType.Date)]
    public DateTime VotingEnd { get; set; }

    public byte[] Logo { get; set; }
    public MeetupType Type { get; set; }
}