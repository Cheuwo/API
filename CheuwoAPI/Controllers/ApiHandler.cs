using Microsoft.AspNetCore.Mvc;

namespace CheuwoAPI.Controllers
{
    public class ApiHandler : ControllerBase
    {
        protected BadRequestObjectResult ApiBadRequest(string message)
        {
            var error = new
            {
                type = 400,
                title = message
            };
            return BadRequest(error);
        }
    }
}
