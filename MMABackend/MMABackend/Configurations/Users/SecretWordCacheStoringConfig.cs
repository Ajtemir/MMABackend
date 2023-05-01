namespace MMABackend.Configurations.Users
{
    public static class SecretWordCacheStoringConfig
    {
        public const int LifeTimeInMinutes = 60;
        private const string PrefixOfIdForReset = "Reset_";
        private const string PrefixOfIdForConfirmEmail = "ConfirmPassword_";
        public static string JoinPrefixAndEmailForReset(string email) => PrefixOfIdForReset + email;
        public static string JoinPrefixAndEmailForConfirmEmail(string email) => PrefixOfIdForConfirmEmail + email;
    }
}