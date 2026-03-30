using KASHOP.BLL.Service;
using KASHOP.DAL.Dto.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace KASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationUsers _authenticationUsers;
        public AccountController(IAuthenticationUsers authenticationUsers)
        {
            _authenticationUsers = authenticationUsers;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result =await _authenticationUsers.RegisterAsync(request);
            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result =await _authenticationUsers.LoginAsync(request);
            return Ok(result);
        }
        [HttpGet("ConfirmEmail")]
        public async Task <IActionResult> ConfirmEmail(String token,string userId)
        {
            var isConfirmedEmail = await _authenticationUsers.confirmEmailAsync(token, userId);
            if (isConfirmedEmail) return Ok();
            return BadRequest();

        }
        [HttpPost("SendCode")]
        public async Task<IActionResult>RequestPasswordResset(ForgotPasswordRequest request)
        {
            var result = await _authenticationUsers.RequestPaaswordRerset(request);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
            
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResertPassword(ResetPasswordRequest request)
        {
            var result = await _authenticationUsers.ResetPasswordAsync(request);
            if (!result.Success) return BadRequest(result);
            return Ok(result);

        }

    }
}
