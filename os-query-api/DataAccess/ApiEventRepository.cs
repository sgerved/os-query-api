using Microsoft.EntityFrameworkCore;
using os_query_api.DataAccess.Models;

namespace os_query_api.DataAccess;

public class ApiEventRepository(ApiEventsDbContext context) : IApiEventRepository
{
    public async Task LogAsync(ApiEventModel apiEvent)
    {
        context.ApiEvents.Add(apiEvent);
        await context.SaveChangesAsync();
    }

    public async Task<List<ApiEventModel>> GetAllAsync()
    {
        return await context.ApiEvents.ToListAsync();
    }
}
