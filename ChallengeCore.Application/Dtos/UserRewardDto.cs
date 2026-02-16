namespace ChallengeCore.Application.Dtos
{
    public class UserRewardDto
    {
        public Guid RewardId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Cost { get; set; }
        
    }
}
