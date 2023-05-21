using Microsoft.AspNetCore.Mvc;
using User.Business.Interfaces;

namespace User.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("retrieveUsers")]
        public async Task<IActionResult> RetrieveUsers([FromBody] List<string> usernames)
        {
            if (usernames == null || !usernames.Any())
            {
                return BadRequest("Invalid list of username");
            }

            try
            {
                var result = await _userService.GetUserDetails(usernames);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
