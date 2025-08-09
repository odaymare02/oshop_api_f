using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oshop.BLL.Services.Interfaces;
using Oshop.DAL.DTO.Requests;
using System.Threading.Tasks;

namespace Oshop.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles ="Admin,SuperAdmin")]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        [HttpGet("")]
        public IActionResult getAll() => Ok(_brandService.GetALl());
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var brand = _brandService.GetById(id);
            if (brand is null) return NotFound();
            return Ok(brand);
        }
        [HttpPost]
        public async Task<IActionResult> createBrand([FromForm] BrandRequest request)
        {
            var id = await _brandService.CreatWithFile(request);
            return CreatedAtAction(nameof(Get), new { id }, (""));
        }
        [HttpPatch("{id}")]
        public IActionResult UpdateName([FromRoute] int id, [FromBody] BrandRequest request)
        {
            var updated = _brandService.Update(id, request);
            return updated > 0 ? Ok() : NotFound();
        }
        [HttpPatch("ToogleStatus/{id}")]
        public IActionResult UpdateToggle([FromRoute] int id)
        {
            var updated = _brandService.ToogleStatus(id);
            return updated ? Ok() : NotFound();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var deleted = _brandService.Delete(id);
            return deleted > 0 ? Ok() : NotFound();
        }
    }
}
