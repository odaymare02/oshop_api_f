using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oshop.BLL.Services.Interfaces;
using Oshop.DAL.DTO.Requests;

namespace Oshop.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("")]
        public IActionResult GetAll()=>Ok(_categoryService.GetALl());
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var category = _categoryService.GetById(id);
            if (category is null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPost]
        public IActionResult Create([FromBody] CategoryRequest request)
        {
            var id = _categoryService.Create(request);
            return CreatedAtAction(nameof(Get), new { id },(""));//return 201 and link with details of this category
        }
        [HttpPatch("{id}")]
        public IActionResult UpdateName([FromRoute] int id, [FromBody] CategoryRequest request)
        {
            var updated = _categoryService.Update(id, request);
            return updated > 0 ? Ok() : NotFound();
        }
        [HttpPatch("ToogleStatus/{id}")]
        public IActionResult UpdateToggle([FromRoute] int id)
        {
            var updated = _categoryService.ToogleStatus(id);
            return updated ? Ok() : NotFound();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var deleted = _categoryService.Delete(id);
            return deleted > 0 ? Ok() : NotFound();
        }
    }
}
