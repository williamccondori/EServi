using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using EServi.Microservices.Auth.UseCase.Models;
using EServi.Microservices.Auth.UseCase.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EServi.Microservices.Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;

        private readonly IAuthService _authService;

        public AuthController(
            ILogger<AuthController> logger,
            IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] Login login)
        {
            _logger.LogTrace(nameof(Authenticate));

            try
            {
                var authToken = await _authService.Authenticate(login);

                return Ok(authToken);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}