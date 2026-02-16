using ChallengeCore.Domain.Exceptions;
using ChallengeCore.Application.Interfaces;
using ChallengeCore.Infrastructure.Configuration;
using ChallengeCore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using static ChallengeCore.Domain.Enums;
using ChallengeCore.Application.Dtos;

namespace ChallengeCore.Infrastructure.Services
{
    public class ChallengeService(
        ApplicationDbContext context,
        IOptions<Features> features
        ) : IChallengeService
    {

        private readonly ApplicationDbContext _context = context;
        private readonly Features _features = features.Value;

        public async Task<List<UserChallengeDto>> GetUserChallengesAsync(Guid userId)
        {
            var challenges = await _context.UserChallenges
               .Include(uc => uc.Challenge)
               .Where(uc => uc.UserId == userId)
               .Select(uc => new UserChallengeDto
               {
                   ChallengeId = uc.ChallengeId,
                   Title = uc.Challenge.Title,
                   Description = uc.Challenge.Description,
                   Points = uc.Challenge.Points,
                   Status = uc.Status,
                   IsBonus = uc.Challenge.IsBonus
               }).ToListAsync();

            return challenges;
        }

        public async Task CompleteChallengeAsync(Guid userId, Guid challengeId)
        {
            var userChallenge = await _context.UserChallenges
                 .Include(uc => uc.Challenge)
                 .FirstOrDefaultAsync(uc =>
                     uc.UserId == userId &&
                     uc.ChallengeId == challengeId);

            if (userChallenge == null)
                throw new BusinessException("Reto no encontrado para este usuario");

            if (userChallenge.Status == ChallengeStatus.Completed)
                throw new BusinessException("El reto ya fue completado");

            userChallenge.Status = ChallengeStatus.Completed;
            userChallenge.CompletedAt = DateTime.UtcNow;

            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                throw new BusinessException("Usuario no encontrado");

            user.Points += userChallenge.Challenge.Points;

            await _context.SaveChangesAsync();
        }

       
    }
}
