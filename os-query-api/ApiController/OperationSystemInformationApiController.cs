using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace os_query_api.ApiController
{
    [Route("api/os/v1/operation-system-information")]
    [ApiController]
    public class OperationSystemInformationApiController : ControllerBase
    {
        [Route("ping")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Operation System Information API");
        }
    }
}
