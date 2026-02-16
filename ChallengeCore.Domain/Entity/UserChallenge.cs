using static ChallengeCore.Domain.Enums;

namespace ChallengeCore.Domain.Entity
{
    public class UserChallenge
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public Guid ChallengeId { get; set; }
        public Challenge Challenge { get; set; } = null!;
        public ChallengeStatus Status { get; set; } = ChallengeStatus.Pending;
        public DateTime? CompletedAt { get; set; }
    }
}
