using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using os_query_api.BusinessLogic;

namespace os_query_api.ApiController
{
    [Route("api/os/v1/test-endpoint")]
    [ApiController]
    public class TestApiController(ITokenCacheSingleton tokenCache) : ControllerBase
    {
        public IActionResult Get()
        {
            int random = new Random().Next(1, 100);
            tokenCache.SetToken("random", random.ToString());
            
            return Ok($"{tokenCache.GetToken("random")} : {random}");
        }
    }
}
