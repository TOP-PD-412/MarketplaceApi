using System.ComponentModel.DataAnnotations;

namespace UsersAPI.Auth;

public sealed record LoginRequest
{
    [Required]
    public required string PhoneNumber { get; init; }
    [Required]
    public required string Password { get; init; }
}