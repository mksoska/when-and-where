﻿using WhenAndWhere.BL.Interfaces;

namespace WhenAndWhere.BL.DTOs;

public class OptionDTO : IDto
{
    public int Id { get; set; }
    public int MeetupId { get; set; }
    public int OwnerId { get; set; }
    public string Label { get; set; }
    
    public string State { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string ZipCode { get; set; }
    
    public DateTime Start { get; set; }
    public DateTime End { get; set; }

}