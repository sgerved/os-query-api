using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using os_query_api.DataAccess;

namespace os_query_api.ApiController
{
    [Route("api/os/v1/api-events")]
    [ApiController]
    public class ApiEventsApiController(IApiEventRepository apiEventRepository) : ControllerBase
    {
        [Route("get-all-events")]
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            return Ok(await apiEventRepository.GetAllAsync());
        }
    }
}
