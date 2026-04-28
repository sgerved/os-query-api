using os_query_api.DataAccess.Models;

namespace os_query_api.DataAccess;

public interface IApiEventRepository
{
    Task LogAsync(ApiEventModel apiEvent);
    Task<List<ApiEventModel>> GetAllAsync();
}
