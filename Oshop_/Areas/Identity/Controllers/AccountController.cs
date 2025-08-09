using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Oshop.BLL.Services.Interfaces;
using Oshop.DAL.DTO.Requests;
using Oshop.DAL.DTO.Responses;
using System.Threading.Tasks;

namespace Oshop.PL.Areas.Identity.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Identity")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserResponse>> Register(DAL.DTO.Requests.RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);
            return Ok(result);
        }
        [HttpGet("ConfirmEmail")]
        public async Task<ActionResult<string>> ConfirmEmail([FromQuery] string token, [FromQuery] string userId)
        {
            var result = await _authService.ConfirmEmail(token,userId);
            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserResponse>> Login(DAL.DTO.Requests.LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);
            return Ok(result);
        }
        [HttpPost("ForgetPassword")]
        public async Task<ActionResult<string>> ForgetPassword([FromBody]ForgetPasswordRequest request)
        {
            var result = await _authService.ForgetPassword(request);
            return Ok(result);
        }
        [HttpPatch("ResetPassword")]
        public async Task<ActionResult<string>> ResetPassword([FromBody] DAL.DTO.Requests.ResetPasswordRequest request)
        {
            var result = await _authService.ResetPassword(request);
            return Ok(result);
        }
    }

}
