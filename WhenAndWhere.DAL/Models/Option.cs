using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhenAndWhere.DAL.Models;

public class Option : IEntity
{
    [Key]
    public int Id { get; set; }

    public int MeetupId { get; set; }
    
    [ForeignKey(nameof(MeetupId))]
    public virtual Meetup Meetup { get; set; }
    
    public int UserId { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public virtual User Owner { get; set; }
    
    public virtual Address Address { get; set; }

    public string Label { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public virtual List<UserOption> Voters { get; set; }
}