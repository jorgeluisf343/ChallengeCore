using ChallengeCore.Application.Interfaces;
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
            await _challengeService.CompleteChallengeAsync(userId, challengeId);
            return NoContent();
        }

        [HttpGet("users/{userId}")]
        public async Task<IActionResult> GetUserChallenges(Guid userId)
        {
            var challenges = await _challengeService.GetUserChallengesAsync(userId);
            return Ok(challenges);
        }


    }
}
