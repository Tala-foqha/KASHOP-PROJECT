using KASHOP.DAL.Dto.Request;
using KASHOP.DAL.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service
{
    public interface IAuthenticationUsers
    {
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
        public  Task<ForgotPasswordResponse> RequestPaaswordRerset(ForgotPasswordRequest request);
        Task<LoginResponse> LoginAsync(LoginRequest request);
        public Task<bool> confirmEmailAsync(String token, String userId);
        public Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
    }
}
