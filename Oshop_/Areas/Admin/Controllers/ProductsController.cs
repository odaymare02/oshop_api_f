using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oshop.BLL.Services.Interfaces;
using Oshop.DAL.DTO.Requests;
using Oshop.DAL.DTO.Responses;
using System.Threading.Tasks;

namespace Oshop.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles ="Admin,SuperAdmin")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost("")]
        public async Task<IActionResult> Create([FromForm]ProductRequest request)
        {
            var res =  await _productService.CreatWithFile(request);
            return Ok(res);
        }
    }
}
