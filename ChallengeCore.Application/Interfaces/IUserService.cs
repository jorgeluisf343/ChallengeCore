using ChallengeCore.Application.Dtos;

namespace ChallengeCore.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserProfileDto> CreateUserAsync(CreateUserDto dto);
        Task<UserProfileDto?> GetUserProfileAsync(Guid userId);
    }
}
