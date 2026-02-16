using ChallengeCore.Application.Dtos;

namespace ChallengeCore.Application.Interfaces
{
    public interface IRewardService
    {
        Task<List<RewardDto>> GetRewardsAsync();
        Task<List<UserRewardDto>> GetUserRewardsAsync(Guid userId);
        Task RedeemRewardAsync(Guid userId, Guid rewardId);
    }
}
