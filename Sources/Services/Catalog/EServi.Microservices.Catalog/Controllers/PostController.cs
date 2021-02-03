using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using EServi.Microservices.Catalog.UseCase.Models;
using EServi.Microservices.Catalog.UseCase.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EServi.Microservices.Catalog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly ILogger<PostController> _logger;

        private readonly ICatalogService _catalogService;

        public PostController(
            ILogger<PostController> logger,
            ICatalogService catalogService)
        {
            _logger = logger;
            _catalogService = catalogService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] PostModel postModel)
        {
            _logger.LogTrace(nameof(CreatePost));

            try
            {
                var post = await _catalogService.CreatePost(postModel);
                return Created(string.Empty, post);
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