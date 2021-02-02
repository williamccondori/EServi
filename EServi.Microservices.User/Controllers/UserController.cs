using System;
using System.ComponentModel.DataAnnotations;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInfoById([FromQuery] string id)
        {
            _logger.LogTrace(nameof(GetInfoById));

            try
            {
                var userInfo = await _userService.GetInfoById(id);

                return Ok(userInfo);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfile([FromQuery] string id, [FromBody] UserProfile userProfile)
        {
            _logger.LogTrace(nameof(UpdateProfile));

            try
            {
                var userProfileModified = await _userService.UpdateProfile(id, userProfile);

                return Ok(userProfileModified);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegister userRegister)
        {
            _logger.LogTrace(nameof(GetInfoById));

            try
            {
                var newUserRegister = await _userService.Register(userRegister);

                return Ok(newUserRegister);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}