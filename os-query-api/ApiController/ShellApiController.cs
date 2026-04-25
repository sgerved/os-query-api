using Microsoft.AspNetCore.Mvc;
using os_query_api.BusinessLogic;

namespace os_query_api.ApiController
{
    [Route("api/os/v1/shell")]
    [ApiController]
    public class ShellApiController(IShell shell) : ControllerBase
    {
        [Route("run/{command}")]
        [HttpGet]
        public IActionResult Run(string command)
        {
            return Ok(shell.Run(command));
        }
    }
}
