using Oshop.DAL.DTO.Requests;
using Oshop.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oshop.BLL.Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserResponse> LoginAsync(LoginRequest request);
        Task<UserResponse> RegisterAsync(RegisterRequest request);
        Task<string> ConfirmEmail(string token, string userId);
        Task<bool> ForgetPassword(ForgetPasswordRequest request);
        Task<bool> ResetPassword(ResetPasswordRequest request);
    }
}
