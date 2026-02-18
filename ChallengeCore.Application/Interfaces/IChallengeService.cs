using ChallengeCore.Application.Dtos;

namespace ChallengeCore.Application.Interfaces
{
    public interface IChallengeService
    {
        Task<SuccessDto> CompleteChallengeAsync(Guid userId, Guid challengeId);
        Task<List<UserChallengeDto>> GetUserChallengesAsync(Guid userId);

    }
}
