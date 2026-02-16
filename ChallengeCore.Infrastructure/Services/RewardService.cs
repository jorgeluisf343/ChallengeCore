using ChallengeCore.Application.Dtos;
using ChallengeCore.Application.Interfaces;
using ChallengeCore.Domain.Entity;
using ChallengeCore.Domain.Exceptions;
using ChallengeCore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChallengeCore.Infrastructure.Services
{
    public class RewardService(ApplicationDbContext context) : IRewardService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<List<RewardDto>> GetRewardsAsync()
        {
            return await _context.Rewards
                .Select(r => new RewardDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    Cost = r.Cost
                }).ToListAsync();
        }

        public async Task<List<UserRewardDto>> GetUserRewardsAsync(Guid userId)
        {
            var rewards = await _context.UserRewards
               .Include(uc => uc.Reward)
               .Where(uc => uc.UserId == userId)
               .Select(uc => new UserRewardDto
               {
                   RewardId = uc.RewardId,
                   Name = uc.Reward.Name,
                   Description = uc.Reward.Description,
                   Cost = uc.Reward.Cost
               }).ToListAsync();
            return rewards;
        }

        public async Task RedeemRewardAsync(Guid userId, Guid rewardId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            var user = await _context.Users.FindAsync(userId);
            var reward = await _context.Rewards.FindAsync(rewardId);

            if (user == null)
                throw new BusinessException("Usuario no encontrado");

            if (reward == null)
                throw new BusinessException("Recompensa no encontrada");

            if (user.Points < reward.Cost)
                throw new BusinessException("No tienes puntos suficientes");

            try
            {
                user.Points -= reward.Cost;
                _context.UserRewards.Add(new UserReward
                {
                    UserId = userId,
                    RewardId = rewardId,
                    RedeemedAt = DateTime.UtcNow
                });

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw new BusinessException("Error en la transaccion");
            }

        }
    }
}
