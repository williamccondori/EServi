using System;
using System.Threading.Tasks;
using EServi.Microservices.User.UseCase.Models;
using EServi.Microservices.User.UseCase.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EServi.Microservices.User.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        private readonly IUserService _userService;

        public UserController(
            ILogger<UserController> logger,
            IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetInfoById([FromQuery] Guid userId)
        {
            _logger.LogTrace(nameof(GetInfoById));

            try
            {
                var userInfo = await _userService.GetInfoById(userId);

                return Ok(userInfo);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfile([FromQuery] Guid userId, [FromBody] UserProfile userProfile)
        {
            _logger.LogTrace(nameof(UpdateProfile));

            try
            {
                var userProfileModified = await _userService.UpdateProfile(userId, userProfile);

                return Ok(userProfileModified);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}