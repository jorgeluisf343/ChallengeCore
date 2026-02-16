namespace ChallengeCore.Domain.Entity
{
    public class UserReward
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public Guid RewardId { get; set; }
        public Reward Reward { get; set; } = null!;
        public DateTime RedeemedAt { get; set; } = DateTime.UtcNow;
    }
}
