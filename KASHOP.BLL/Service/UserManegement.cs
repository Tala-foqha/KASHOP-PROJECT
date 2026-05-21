using KASHOP.DAL.Dto.Response;
using KASHOP.DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service
{
    public class UserManegement : IUserManagement
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserManegement(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;

        }

        public async Task<bool> ChangeRole(string userId, string Role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roleExists = await _roleManager.RoleExistsAsync(Role);
            if (!roleExists) return false;
            var currentRole = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user,currentRole);
            var result = await _userManager.AddToRoleAsync(user, Role);
            return result.Succeeded;
        }

        public Task<bool> DeleteUser(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserListResponse>> GetAllUser()
        {
            var users = await _userManager.Users.ToListAsync();
            return users.Adapt<List<UserListResponse>>();
        }

        public async Task<UserDetailsResponse> GetUser(string uderId)
        {
            var user = await _userManager.FindByIdAsync(uderId);
         var roles=   await _userManager.GetRolesAsync(user);
            var result= user.Adapt<UserDetailsResponse>();
            result.Role = roles.FirstOrDefault();//هاي ليست بس الي احنا عنا بس ستريم الي هي صلاحية وحدة فبنعمل هيك وبنجيب اول صلاحية وخلص

            return result;
        }

        public Task<bool> ToggleBlockUser(string userId)
        {
            throw new NotImplementedException();
        }

        //public async Task<bool> ToggleBlockUser(string userId)
        //{
        //    var user = await _userManager.FindByIdAsync(userId);
        //    bool isBlocked = user.LockoutEnd > DateTime.UtcNow;
        //    if (isBlocked)
        //    {
        //        await 
        //    }
        //    //block user
        //    else
        //    {
        //        await _userManager.SetLockoutEnabledAsync(user, true);


        //        await _userManager.SetLockoutEndDateAsync(user, DateTime.UtcNow.AddDays(5));
        //    } }
    }
}
