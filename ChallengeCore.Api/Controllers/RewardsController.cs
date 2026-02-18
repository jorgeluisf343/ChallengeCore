using ChallengeCore.Api.Response;
using ChallengeCore.Application.Dtos;
using ChallengeCore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeCore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RewardsController(IRewardService rewardService) : ControllerBase
    {
        private readonly IRewardService _rewardService = rewardService;

        [HttpGet]
        public async Task<IActionResult> GetRewards()
        {
            var rewards = await _rewardService.GetRewardsAsync();
            return Ok(new ApiResponse<IEnumerable<RewardDto>>(rewards));
        }

        [HttpPost("redeem/{rewardId}")]
        public async Task<IActionResult> RedeemReward(Guid rewardId, [FromQuery] Guid userId)
        {
            var completed = await _rewardService.RedeemRewardAsync(userId, rewardId);
            return Ok(new ApiResponse<SuccessDto>(completed));
        }

        [HttpGet("users/{userId}")]
        public async Task<IActionResult> GetUserRewards(Guid userId)
        {
            var rewards = await _rewardService.GetUserRewardsAsync(userId);
            return Ok(new ApiResponse<IEnumerable<UserRewardDto>>(rewards));
        }
    }
}
