using CoreLib.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CoreLib.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class CommonController : ControllerBase
    {
        [HttpGet("{*url}", Order = int.MaxValue)]
        public NotFound Index()
        {
            throw new NotFound(4040);
        }

        [HttpGet("/status")]
        public IActionResult Status()
        {
            return Ok(new { Status = "OK" });
        }
    }
}