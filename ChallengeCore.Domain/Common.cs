namespace ChallengeCore.Domain
{
    public class Common
    {
        public enum ChallengeStatus
        {
            Pending = 0,
            Completed = 1
        }

        public static class Constants
        {
            public const string Simple = "simple";
            public const string Experto = "experto";
            public const string Seleccion = "seleccion";
            public const string Bonus = "bonus";
        }
    }
}
