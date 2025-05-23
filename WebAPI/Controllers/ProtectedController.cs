using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProtectedController : ControllerBase
    {
        [Authorize]
        [HttpGet("data")]
        public IActionResult GetProtectedData()
        {
            return Ok(new { message = "This is protected data." });
        }
    }
}
