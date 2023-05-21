using System.Text;
using Azure;
using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using WhenAndWhere.BL.DTOs;

namespace WhenAndWhere.BL.Services;

public class TableMeetupService
{
    private readonly TableClient _tableClient;
    private readonly QueueClient _queueClient;

    public TableMeetupService(TableClient tableClient, QueueClient queueClient)
    {
        _queueClient = queueClient;
        _tableClient = tableClient;
    }

    public List<TableMeetupDTO> SearchInvited(int userId, string searchString, int requestedPageNumber, int pageSize, out int totalItemsCount)
    {
        // Partition key = userId, Row key = meetupId
        var meetups = _tableClient.Query<TableMeetupDTO>(
            ent => ent.PartitionKey == userId.ToString() && 
                   (string.IsNullOrEmpty(searchString) || 
                    string.Compare(ent.Name, searchString, StringComparison.OrdinalIgnoreCase) < 0));
        
        totalItemsCount = meetups.Count();
        return meetups.AsPages(null, pageSize)
            .ElementAt(requestedPageNumber - 1).Values
            .OrderByDescending(i => i.DateInvited)
            .ToList();
    }

    public void SetMeetup(TableMeetupDTO meetup)
    {
        _queueClient.SendAsync(new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(meetup))));
    }

    public TableMeetupDTO GetMeetup(int userId, int meetupId)
    {
        return _tableClient.Query<TableMeetupDTO>(ent =>
            ent.PartitionKey == userId.ToString() && ent.RowKey == meetupId.ToString())
            .First();
    }
}