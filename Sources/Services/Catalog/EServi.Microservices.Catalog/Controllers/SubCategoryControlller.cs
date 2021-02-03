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
    public class SubCategoryController : ControllerBase
    {
        private readonly ILogger<SubCategoryController> _logger;

        private readonly ICatalogService _catalogService;

        public SubCategoryController(
            ILogger<SubCategoryController> logger,
            ICatalogService catalogService)
        {
            _logger = logger;
            _catalogService = catalogService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SubCategoryModel subCategoryModel)
        {
            _logger.LogTrace(nameof(Create));

            try
            {
                var subCategory = await _catalogService.CreateSubCategory(subCategoryModel);
                return Created(string.Empty, subCategory);
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