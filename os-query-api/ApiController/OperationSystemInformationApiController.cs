using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using os_query_api.BusinessLogic;

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
            var _logic = new OperationSystemInformationLogic();
            return Ok(_logic.Ping());
        }
        
        [Route("version")]
        [HttpGet]
        public IActionResult GetOsVersion()
        {
            var _logic = new OperationSystemInformationLogic();
            return Ok(_logic.GetOsVersion());
        }
    }
}
