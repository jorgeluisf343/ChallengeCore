namespace ChallengeCore.Infrastructure.Configuration
{

    public class DatabaseOptions
    {
        public string ServerName { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int TimeOut { get; set; } = 0;
    }

    public class Features
    {
        public bool EnableBonusChallenge { get; set; }
    }
}
