namespace ChallengeCore.Application.Dtos
{
    public class RewardDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Cost { get; set; }
    }
}
