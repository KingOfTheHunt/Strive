using System.Text.Json.Serialization;
using Flunt.Notifications;

namespace Strive.Application.UseCases.Users.Authenticate;

public record Response(bool IsSuccess, string Message, int StatusCode, IEnumerable<Notification>? Notifications = null)
    : Core.Abstractions.Response(IsSuccess, Message, StatusCode, Notifications)
{
    public ResponseData? Data { get; set; }

    public Response(bool isSuccess, string message, int statusCode, ResponseData data, 
        IEnumerable<Notification>? notifications = null) : this(isSuccess, message, statusCode, notifications)
    {
        Data = data;
    }
}

public record ResponseData
{
    [JsonIgnore]
    public int Id { get; init; }
    [JsonIgnore]
    public string Email { get; init; } = string.Empty;
    [JsonIgnore]
    public string Name { get; init; } = string.Empty;
    public string Token { get; init; } = string.Empty;

    public ResponseData(int id, string email, string name, string token)
    {
        Id = id;
        Email = email;
        Name = name;
        Token = token;
    }
}