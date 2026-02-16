namespace ChallengeCore.Domain.Entity
{
    public class Reward
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Cost { get; set; }
        public ICollection<UserReward> UserRewards { get; set; } = new List<UserReward>();
    }
}
