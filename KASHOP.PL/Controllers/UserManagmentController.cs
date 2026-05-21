using KASHOP.BLL.Service;
using KASHOP.DAL.Dto.Request;
using KASHOP.PL.Resourses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace KASHOP.PL.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize]
    //حاليا ع الي احنا عاملينه اي حدا بقدر يشوف كل اليوزر مش بس الادمن
    public class UserManagmentController : ControllerBase
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IUserManagement _userManagement;
        public UserManagmentController(IUserManagement userManagement, IStringLocalizer<SharedResources> localizer)
        {
            _userManagement = userManagement;
            _localizer = localizer;
        }
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManagement.GetAllUser();
            return Ok(users);
        }

        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userManagement.GetUser(id);
            return Ok(user);

        }
        [HttpPatch("{userId}/role")]
        public async Task<IActionResult> ChangeRole(string userId, [FromBody] ChangeRoleRequest request)
        {
            var result = await _userManagement.ChangeRole(userId, request.newRole);
            if (!result) return BadRequest();
            return Ok();
        }
  


    }
}
