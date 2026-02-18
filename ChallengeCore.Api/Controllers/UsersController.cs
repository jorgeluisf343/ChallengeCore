using ChallengeCore.Api.Response;
using ChallengeCore.Application.Dtos;
using ChallengeCore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto dto)
        {
            var result = await _userService.CreateUserAsync(dto);
            return Ok(new ApiResponse<UserProfileDto>(result));
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> GetUserProfile(Guid id)
        {
            var user = await _userService.GetUserProfileAsync(id);
            if (user == null)
                return NotFound();
            return Ok(new ApiResponse<UserProfileDto>(user));
        }
    }
}
