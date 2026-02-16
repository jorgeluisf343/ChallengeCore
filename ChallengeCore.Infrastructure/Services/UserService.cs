using Microsoft.EntityFrameworkCore;
using ChallengeCore.Application.Dtos;
using ChallengeCore.Domain.Entity;
using ChallengeCore.Domain.Exceptions;
using ChallengeCore.Infrastructure.Configuration;
using ChallengeCore.Infrastructure.Data;
using Microsoft.Extensions.Options;
using static ChallengeCore.Domain.Enums;
using ChallengeCore.Application.Interfaces;

namespace ChallengeCore.Infrastructure.Services
{
    public class UserService(
        ApplicationDbContext context,
        IOptions<Features> features) : IUserService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly Features _features = features.Value;

        public async Task<UserProfileDto> CreateUserAsync(CreateUserDto dto)
        {
            if (string.IsNullOrEmpty(dto.Name))
                throw new BusinessException("El nombre es obligatorio");

            var user = new User
            {
                Name = dto.Name,
                AvatarUrl = dto.AvatarUrl, 
                Points = 0
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            
            var challengesQuery = _context.Challenges.AsQueryable();

            if (!_features.EnableBonusChallenge)
            {
                challengesQuery = challengesQuery.Where(c => !c.IsBonus);
            }

            //obtener lista de retos
            var challenges = await challengesQuery.ToListAsync();

            var userChallenges = challenges.Select(c => new UserChallenge
            {
                UserId = user.Id,
                ChallengeId = c.Id,
                Status = ChallengeStatus.Pending
            });

            _context.UserChallenges.AddRange(userChallenges);
            await _context.SaveChangesAsync();

            return new UserProfileDto
            {
                Id = user.Id,
                Name = user.Name,
                AvatarUrl = user.AvatarUrl
            };
        }

        public async Task<UserProfileDto?> GetUserProfileAsync(Guid userId)
        {
            var user = await _context.Users
                    .Include(u => u.UserChallenges)
                    .Include(u => u.UserRewards)
                    .ThenInclude(ur => ur.Reward)
                    .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) return null;

            return new UserProfileDto
            {
                Id = user.Id, //identificador
                Name = user.Name, //nombre
                AvatarUrl = user.AvatarUrl, //imagen
                Points = user.Points, //puntos acumulados
                CompletedChallenges = user.UserChallenges.Count(uc => uc.Status == ChallengeStatus.Completed), //cantidad de retos completados
                TotalChallenges = user.UserChallenges.Count, 
                Rewards = user.UserRewards
                    .Select(ur => ur.Reward.Name)
                    .ToList() // lista de recompensas canjeadas
            };
        }
    }
}
