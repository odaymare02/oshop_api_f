using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oshop.BLL.Services.Interfaces;

namespace Oshop.PL.Areas.Customer.Controllers
{
    [Route("api/[Area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles ="Customer")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("")]
        public IActionResult getAll() => Ok(_categoryService.GetALl(true));
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var category = _categoryService.GetById(id);
            if (category is null) return NotFound();
            return Ok(category);
        }
    }
}
