using Microsoft.AspNetCore.Mvc;
using Proj.Util;

namespace Pos.Api.Helper;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    // Helper to convert DataResult to ActionResult
    protected ActionResult HandleResult<T>(DataResult<T> result)
    {
        // Returns 200 with the DataResult object
        if (result.IsSuccess)
        {
            return Ok(result);
        }

        if (result.Message.Contains("Database") || result.Message.Contains("Connection"))
        {
            // Internal Server Error
            return StatusCode(505, result);
        }

        // Returns 400 with the error message
        return BadRequest(result); 
    }
}