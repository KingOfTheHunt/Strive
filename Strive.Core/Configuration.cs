namespace Strive.Core;

public static class Configuration
{
    public static DatabaseConnection Database { get; private set; } = new();
}

public class DatabaseConnection
{
    public string Connection { get; set; } = string.Empty;
}
