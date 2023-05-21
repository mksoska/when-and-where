using System;
using System.Threading.Tasks;
using Azure.Data.Tables;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WhenAndWhere.FunctionApp;

public static class MeetupTableWrite
{
    [FunctionName("MeetupTableWrite")]
    public static async Task RunAsync([ServiceBusTrigger("meetup-table-queue", Connection = "ServiceBusConnectionString")] string message, ILogger log)
    {
        var tableClient = new TableClient(
            new Uri("https://485607.table.core.windows.net/WhenAndWhere"),
            "WhenAndWhere",
            new TableSharedKeyCredential("485607",
                "wOX2fcfPw5x6lGNI1x1rV5XR+b8eEgLRkWXH6en+qNIU36xxRBOcK8fd7aX1PkVXJcux5O63KiBu+AStpHj7mA=="));

        var meetup = JsonConvert.DeserializeObject<TableMeetupDTO>(message);
        meetup.DateInvited = DateTime.SpecifyKind(meetup.DateInvited, DateTimeKind.Utc);
        meetup.VotingEnd = DateTime.SpecifyKind(meetup.VotingEnd, DateTimeKind.Utc);
        await tableClient.UpsertEntityAsync(meetup);
        
        log.LogInformation($"C# ServiceBus queue trigger function processed message: {message}");
    }
}