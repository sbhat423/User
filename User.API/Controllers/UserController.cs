using Microsoft.AspNetCore.Mvc;
using User.Business.Interfaces;

namespace User.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("retrieveUsers")]
        public async Task<IActionResult> RetrieveUsers([FromBody] List<string> usernames)
        {
            if (usernames == null || !usernames.Any())
            {
                return BadRequest("The list of usernames is null or empty");
            }
            if (usernames.Exists(x => string.IsNullOrEmpty(x)))
            {
                return BadRequest("The usernames list item should not contain null or empty values");
            }

            try
            {
                var result = await _userService.GetUserDetails(usernames);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request");
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
