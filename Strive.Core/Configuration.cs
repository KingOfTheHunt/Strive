namespace Strive.Core;

public static class Configuration
{
    public static string JwtKey { get; set; }
    public static DatabaseConnection Database { get; private set; } = new();
    public static Smtp Smtp { get; private set; } = new();
}

public class DatabaseConnection
{
    public string Connection { get; set; } = string.Empty;
}

public class Smtp
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
