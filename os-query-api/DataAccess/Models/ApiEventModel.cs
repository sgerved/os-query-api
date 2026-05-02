using Microsoft.EntityFrameworkCore;

namespace os_query_api.DataAccess.Models;

[PrimaryKey("EventId")]
public class ApiEventModel
{
    public int EventId { get; set; }
    public string EventName { get; set; } = "";
    public string EventData { get; set; } = "";
    public DateTime EventTime { get; set; }
    
    public override string ToString()
    {
        return $"EventId: {EventId}, EventName: {EventName}, EventData: {EventData}, EventTime: {EventTime}";
    }
}