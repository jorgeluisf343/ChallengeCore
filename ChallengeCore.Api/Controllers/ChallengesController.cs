using ChallengeCore.Api.Response;
using ChallengeCore.Application.Dtos;
using ChallengeCore.Application.Interfaces;
using ChallengeCore.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeCore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChallengesController(IChallengeService challengeService) : ControllerBase
    {
        private readonly IChallengeService _challengeService = challengeService;

        [HttpPost("complete/{challengeId}")]
        public async Task<IActionResult> CompleteChallenge(Guid challengeId, [FromQuery] Guid userId)
        {
            var completed = await _challengeService.CompleteChallengeAsync(userId, challengeId);
            return Ok(new ApiResponse<SuccessDto>(completed));
        }

        [HttpGet("users/{userId}")]
        public async Task<IActionResult> GetUserChallenges(Guid userId)
        {
            var challenges = await _challengeService.GetUserChallengesAsync(userId);
            return Ok(new ApiResponse<IEnumerable<UserChallengeDto>>(challenges));
        }
    }
}
