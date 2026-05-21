using KASHOP.DAL.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service
{
    public interface IUserManagement
    {
        Task<List<UserListResponse>> GetAllUser();
        Task<UserDetailsResponse> GetUser(string uderId);
        Task<bool> ChangeRole(string userId, string Role);
        Task<bool>ToggleBlockUser(string userId);
        Task<bool> DeleteUser(string userId);

    }
}
