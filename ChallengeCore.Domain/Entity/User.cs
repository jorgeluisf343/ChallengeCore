using System.ComponentModel.DataAnnotations;

namespace ChallengeCore.Domain.Entity
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string AvatarUrl { get; set; } = string.Empty;

        public int Points { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<UserChallenge> UserChallenges { get; set; } = [];

        public ICollection<UserReward> UserRewards { get; set; } = [];
    }
}
