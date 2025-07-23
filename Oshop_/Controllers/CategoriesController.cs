using Microsoft.AspNetCore.Mvc;
using Oshop.BLL.Services;
using Oshop.DAL.DTO.Requests;

namespace Oshop.PL.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        [HttpGet("")]
        public IActionResult GetAll()
        {
            return Ok(categoryService.GetALlCategories());
        }
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute]int id)
        {
            var category = categoryService.GetCategoryById(id);
            if(category is null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPost]
        public IActionResult Create([FromBody] CategoryRequest request)
        {
            var id=categoryService.CreateCategory(request);
            return CreatedAtAction(nameof(Get), new { id });//return 201 and link with details of this category
        }
        [HttpPatch("{id}")]
        public IActionResult UpdateName([FromRoute] int id, [FromBody]CategoryRequest request)
        {
           var updated= categoryService.UpdateCategory(id, request);
            return updated > 0 ? Ok() : NotFound();
        }
        [HttpPatch("ToogleStatus/{id}")]
        public IActionResult UpdateToggle([FromRoute] int id)
        {
            var updated = categoryService.ToogleStatus(id);
            return updated ? Ok() : NotFound();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var deleted = categoryService.DeleteCategory(id);
            return deleted > 0 ? Ok() : NotFound();
        }
    }
}
