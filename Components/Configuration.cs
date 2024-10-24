using TaskList.Components.Domain.Main.ValueObjects;

namespace TaskList.Components
{
    public static class Configuration
    {
        public static SecretsConfiguration Secrets { get; set; } = new();
        public static DataBaseConfiguration Database { get; set; } = new();
        public static EmailConfiguration Email { get; set; } = new();

        public static IpConfiguration Ip { get; set; } = new();

        public class DataBaseConfiguration
        {
            public string ConnectionString { get; set; } = string.Empty;
        }

        public class EmailConfiguration
        {
            public string UserName { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public string SmtpServer { get; set; } = string.Empty;
            public int Port { get; set; }

        }

        public class IpConfiguration
        {
            public string IpAddress { get; set; } = string.Empty;
            public string LocalHost { get; set; } = string.Empty;
        }

        public class SecretsConfiguration
        {
            public string ApiKey { get; set; } = string.Empty;
            public string JwtPrivateKey { get; set; } = string.Empty;
            public string PasswordSaltKey { get; set; } = string.Empty;
        }
    }

}
