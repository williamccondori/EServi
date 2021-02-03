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
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;

        private readonly ICatalogService _catalogService;

        public CategoryController(
            ILogger<CategoryController> logger,
            ICatalogService catalogService)
        {
            _logger = logger;
            _catalogService = catalogService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryModel categoryModel)
        {
            _logger.LogTrace(nameof(Create));

            try
            {
                var category = await _catalogService.CreateCategory(categoryModel);
                return Created(string.Empty, category);
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