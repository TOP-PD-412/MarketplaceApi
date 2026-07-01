namespace UsersAPI.Auth;

public sealed record AuthResponse
{
    public required string Token { get; init; }
}