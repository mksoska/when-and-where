using System.ComponentModel.DataAnnotations;
using Mapster;
using WhenAndWhere.BL.DTOs;
using WhenAndWhere.DAL.Enums;

namespace WhenAndWhere.MVC.Models
{
    public class MeetupCreateViewModel
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime OptionsFrom { get; set; }
        public DateTime OptionsTo { get; set; }
        public byte[] Logo { get; set; }
        [Required]
        public MeetupType Type { get; set; }

        public MeetupDTO ToDto() => this.Adapt<MeetupDTO>();
    }
}
