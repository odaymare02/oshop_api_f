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
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        [HttpGet("")]
        public IActionResult getAll() => Ok(_brandService.GetALl(true));
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var brand = _brandService.GetById(id);
            if (brand is null) return NotFound();
            return Ok(brand);
        }
    }
}
