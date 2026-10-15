namespace CommercialSite.WebApp
{
    public class SessionTimeoutSettings
    {
        public int IdleTimeoutMinutes { get; init; } = 15;

        public int WarningSeconds { get; init; } = 60;

        public TimeSpan IdleTimeout => TimeSpan.FromMinutes(IdleTimeoutMinutes);

        public static SessionTimeoutSettings FromConfiguration(IConfiguration configuration)
        {
            var timeout = configuration.GetValue<int?>("Session:IdleTimeoutMinutes") ?? 15;
            var warning = configuration.GetValue<int?>("Session:WarningSeconds") ?? 60;

            // Keep the warning shorter than the timeout itself
            warning = Math.Min(warning, Math.Max(timeout * 60 - 1, 0));

            return new SessionTimeoutSettings { IdleTimeoutMinutes = Math.Max(timeout, 1), WarningSeconds = warning };
        }
    }
}
