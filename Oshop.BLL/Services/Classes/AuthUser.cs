using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Oshop.BLL.Services.Interfaces;
using Oshop.DAL.DTO.Requests;
using Oshop.DAL.DTO.Responses;
using Oshop.DAL.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Oshop.BLL.Services.Classes
{
    public class AuthUser : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;

        public AuthUser(UserManager<ApplicationUser> userManager, IConfiguration configuration, IEmailSender emailSender)
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailSender = emailSender;
        }
        public async Task<UserResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                throw new Exception("Invalid Email Or Password");
            }
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                throw new Exception("please confirm your email");
            }
            var isPassValid = await _userManager.CheckPasswordAsync(user, request.Pasword);
            if (!isPassValid)
            {
                throw new Exception("Invalid email or password");
            }
            var token = await generateToken(user);
            return new UserResponse()
            {
                Token = token
            };
        }
        public async Task<string> ConfirmEmail(string token, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                throw new Exception("user not found");
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return "confirm email success";
            }
            return "email confirmed failed";
        }
        public async Task<UserResponse> RegisterAsync(RegisterRequest request)
        {
            var user = new ApplicationUser()
            {
                Email = request.Email,
                FullName = request.FullName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                var tokenEmail = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var escapToken = Uri.EscapeDataString(tokenEmail);
                var emailUrl = $"https://localhost:7208/api/Identity/Account/ConfirmEmail?token={escapToken}&userId={user.Id}";
                await _emailSender.SendEmailAsync(user.Email, "hello", $"<h1>hi {user.UserName}</h1> <a href=\"{emailUrl}\">confirm</a>");
                return new UserResponse()
                {
                    Token = request.Email
                };
            }
            else
            {
                throw new Exception($"{result.Errors}");
            }
        }
        private async Task<string> generateToken(ApplicationUser user)
        {
            var Userclaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id)
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                Userclaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: Userclaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<bool> ForgetPassword(ForgetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                throw new Exception("user not found");
            }
            var random = new Random();
            var code = random.Next(1000, 9999).ToString();
            user.CodeResetPassword = code;
            user.CodeResetPasswordExpire = DateTime.UtcNow.AddMinutes(15);
            await _userManager.UpdateAsync(user);
            await _emailSender.SendEmailAsync(user.Email, "forgetPassword", $"<p> code is {code}</p>");
            return true;
        }
        public async Task<bool> ResetPassword(ResetPasswordRequest request)
        {
            var user =await _userManager.FindByEmailAsync(request.Email);
            if (user is null) throw new Exception("user not found");
            if (user.CodeResetPassword != request.Code) return false;
            if (user.CodeResetPasswordExpire < DateTime.UtcNow) return false;
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);
            if (result.Succeeded)
            {
                await _emailSender.SendEmailAsync(user.Email,"resete your password","<h1>your password has changed</h1>");
                return true;
            }
            return false;
        }
    }
}
