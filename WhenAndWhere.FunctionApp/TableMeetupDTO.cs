using System;
using System.ComponentModel.DataAnnotations;
using Azure;
using Azure.Data.Tables;
using Newtonsoft.Json;

namespace WhenAndWhere.FunctionApp;

public class TableMeetupDTO : ITableEntity
{
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    
    [JsonIgnore]
    public DateTimeOffset? Timestamp { get; set; }
    [JsonIgnore]
    public ETag ETag { get; set; }
    
    public int OwnerId { get; set; }

    [Required]
    [StringLength(64, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 1)]
    public string Name { get; set; }
    
    [Required]
    public DateTime VotingEnd { get; set; }
    
    public StateEnum State { get; set; }
    public DateTime DateInvited { get; set; }
    
    public int ParticipantsCount { get; set; }
    public int OptionsCount { get; set; }
}