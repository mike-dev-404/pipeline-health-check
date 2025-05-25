using DataManagerService;
using Microsoft.AspNetCore.Mvc;

namespace DataManagerWebApp.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [Route("code/{code}")]
        [HttpGet]
        public IActionResult GetUser(string code)
        {
            try
            {
                var user = _userService.GetUserById(code);
                if (user == null)
                {
                    return NotFound($"User not found: {code}");
                }
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "User not found: {code}", code);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the user: {code}", code);
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("all")]
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all users");
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("nationality/{nationality}")]
        [HttpGet]
        public IActionResult GetUsersbYNationality(string nationality)
        {
            try
            {
                var users = _userService.GetAllUsersByNationality(nationality);
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all users");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
