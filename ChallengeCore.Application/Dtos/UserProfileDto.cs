namespace ChallengeCore.Application.Dtos
{
    public class UserProfileDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
        public int Points { get; set; }
        public int CompletedChallenges { get; set; }
        public int TotalChallenges { get; set; }
        public List<string> Rewards { get; set; } = new();
    }
}
