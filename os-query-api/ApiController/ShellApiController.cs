using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using os_query_api.ApiController.Models;
using os_query_api.BusinessLogic;
using os_query_api.BusinessLogic.Models;
using os_query_api.DataAccess;
using os_query_api.DataAccess.Models;

namespace os_query_api.ApiController
{
    [Route("api/os/v1/shell")]
    [ApiController]
    public class ShellApiController(IShell shell, IApiEventRepository eventRepository) : ControllerBase
    {
        [Authorize(Roles = "admin")]
        [Route("run")]
        [HttpPost]
        public async Task<IActionResult> Run(ExecuteCommandShellDto dto)
        {
            var eventModel = new ApiEventModel {EventName = "ShellCommand", EventData = dto.Command, EventTime = DateTime.UtcNow};
            await eventRepository.LogAsync(eventModel);
            return Ok(await shell.Run(new ExecuteCommandShellModel { Command = dto.Command }));
        }
    }
}
