using System;
using static ChallengeCore.Domain.Common;

namespace ChallengeCore.Application.Dtos
{
    public class UserChallengeDto
    {
        public Guid ChallengeId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Points { get; set; }
        public ChallengeStatus Status { get; set; }
        public string Type { get; set; } = string.Empty;
        public bool IsBonus { get; set; }

    }
}
