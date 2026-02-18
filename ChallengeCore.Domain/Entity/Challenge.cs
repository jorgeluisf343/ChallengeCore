namespace ChallengeCore.Domain.Entity
{
    public class Challenge
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int Points { get; set; }

        public string Type { get; set; } = string.Empty;

        public bool IsBonus { get; set; } = false;

        public ICollection<UserChallenge> UserChallenges { get; set; } = new List<UserChallenge>();
    }
}
