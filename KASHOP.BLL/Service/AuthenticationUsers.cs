
using KASHOP.DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Ini;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using KASHOP.DAL.Dto.Request;
using KASHOP.DAL.Dto.Response;
using System.Diagnostics.Eventing.Reader;


namespace KASHOP.BLL.Service
{
    public class AuthenticationUsers : IAuthenticationUsers
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContext;
        public AuthenticationUsers(UserManager<ApplicationUser> userManager, IEmailSender emailSender, IConfiguration configuration, IHttpContextAccessor httpContext)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _httpContext = httpContext;
            _configuration = configuration;

        }

        public async Task<LoginResponse> LoginAsync(DAL.Dto.Request.LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                return new LoginResponse()
                {
                    Success = false,
                    Message = "Invalid Email"
                };

            }
            // عشان نتأكد الايميل كونفيرم ترو ولا لا
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                return new LoginResponse()
                {
                    Success = false,
                    Message = "Is Not Confirmed Email"
                };

            }
            var result = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!result)
            {
                return new LoginResponse()
                {
                    Success = false,
                    Message = "Invalid password"
                };



            }
            return new LoginResponse()
            {
                Success = true,
                Message = "Success",
                AccessToken = await GenerateAccessToken(user)
            };
        }

        public async Task<RegisterResponse> RegisterAsync(DAL.Dto.Request.RegisterRequest request)
        {
            var user = request.Adapt<ApplicationUser>();

            var userManeger = await _userManager.CreateAsync(user, request.Password);

            if (!userManeger.Succeeded)
            {
                var errors = string.Join(", ", userManeger.Errors.Select(e => e.Description));

                return new RegisterResponse()
                {
                    Success = false,
                    Message = errors,
                    Errors = userManeger.Errors.Select(p => p.Description).ToList()
                };
            }

            await _userManager.AddToRoleAsync(user, "User");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token = Uri.EscapeDataString(token);

            var emailUrl = $"{_httpContext.HttpContext.Request.Scheme}://{_httpContext.HttpContext.Request.Host}/api/Account/ConfirmEmail?token={token}&userId={user.Id}";

            await _emailSender.SendEmailAsync(
                user.Email,
                "Welcome",
                $"<h1>Welcome {request.UserName}</h1>" +
                $"<p>Please confirm your email:</p>" +
                $"<a href=\"{emailUrl}\">Confirm Email</a>"
            );

            return new RegisterResponse()
            {
                Success = true,
                Message = "success"
            };
        }

        public async Task<bool> confirmEmailAsync(String token, String userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null) return false;
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return false;
            }
            return true;
        }
        private async Task<String> GenerateAccessToken(ApplicationUser user)
        {
            var userClaim = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),


            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
        issuer: _configuration["Jwt:Issuer"],
        audience: _configuration["Jwt:Audience"],
        claims: userClaim,
        expires: DateTime.Now.AddDays(5),
        signingCredentials: credentials
        );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public async Task<ForgotPasswordResponse> RequestPaaswordRerset(DAL.Dto.Request.ForgotPasswordRequest request)
        {
            var user =await _userManager.FindByEmailAsync(request.Email);
            if(user is null)
            {
                return new ForgotPasswordResponse()
                {
                    Message = "email not found",
                    Success = false
                };
            }
            var random = new Random();
          var code=  random.Next(1000,9999).ToString();

            user.CodeRequestPassword = code;
            user.PasswordResetCodeExpiry = DateTime.UtcNow.AddMinutes(15);
            await _userManager.UpdateAsync(user);
            await _userManager.UpdateAsync(user);
            await _emailSender.SendEmailAsync(request.Email, "reset password", $"<p>Code is{code}</p>");
            return new ForgotPasswordResponse()
            {
                Success = true,
                Message="code sent to your email"
            };

        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(DAL.Dto.Request.ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {


                return new ResetPasswordResponse()
                {
                    Message = "email not found",
                    Success = false

                }; }
        else if (user.CodeRequestPassword != request.Code)
            {
                return new ResetPasswordResponse()
                {
                    Message = "Invalid Code",
                    Success = false

                };
            }
            else if (user.PasswordResetCodeExpiry < DateTime.UtcNow)
            {
                return new ResetPasswordResponse()
                {
                    Message = "Invalid Expiried",
                    Success = false

                };
            }
            //بتشوف هل الباسورد نفسه ولا لا
       var isSamePassword=     await _userManager.CheckPasswordAsync(user, request.NewPassword);
            if (isSamePassword) {
                return new ResetPasswordResponse()
                {
                    Message = "New password must be different from old password",
                    Success = false

                };
            }
            var token =await _userManager.GeneratePasswordResetTokenAsync(user);
         var res=   await _userManager.ResetPasswordAsync(user,token, request.NewPassword);
            if (!res.Succeeded)
            {
                return new ResetPasswordResponse()
                {
                    Message = "password reset faild",
                    Success = false

                };
            }
            await _emailSender.SendEmailAsync(request.Email, "change Password", $"<p>your password is changed </p>");
            return new ResetPasswordResponse()
            {
                Message = "success",
                Success = true

            };
        }
    }
}
