using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using os_query_api.BusinessLogic;
using os_query_api.DataAccess;
using os_query_api.DataAccess.Models;

namespace os_query_api.ApiController
{
    [Route("api/os/v1/shell")]
    [ApiController]
    public class ShellApiController(IShell shell, ApiEventRepository eventRepository) : ControllerBase
    {
        [Authorize(Roles = "admin")]
        [Route("run/{command}")]
        [HttpGet]
        public async Task<IActionResult> Run(string command)
        {
            var eventModel = new ApiEventModel {EventName = "ShellCommand", EventData = command, EventTime = DateTime.UtcNow};
            await eventRepository.LogAsync(eventModel);
            return Ok(await shell.Run(command));
        }
    }
}
