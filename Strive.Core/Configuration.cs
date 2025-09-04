namespace Strive.Core;

public static class Configuration
{
    public static string PrivateKey { get; set; } = string.Empty;
    public static string JwtKey { get; set; } = string.Empty;
    public static DatabaseConfiguration Database { get; } = new();
    public static SmtpConfiguration Smtp { get; } = new();
}

public class DatabaseConfiguration
{
    public string Connection { get; set; } = string.Empty;
}

public class SmtpConfiguration
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}