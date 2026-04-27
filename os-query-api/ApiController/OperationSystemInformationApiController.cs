using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using os_query_api.BusinessLogic;

namespace os_query_api.ApiController
{
    [Route("api/os/v1/operating-system-information")]
    [ApiController]
    public class OperationSystemInformationApiController(IOperatingSystemInformationLogic logic) : ControllerBase
    {
        [Authorize]
        [Route("ping")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(logic.Ping());
        }
        
        [Authorize]
        [Route("version")]
        [HttpGet]
        public IActionResult GetOsVersion()
        {
            return Ok(logic.GetOsVersion());
        }
    }
}
