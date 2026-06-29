using System.Numerics;
using Shared.Infrastructure;

namespace Shared.Users;

public sealed record UserModel : Model
{
    public string Name { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
    public string? PasswordHash { get; init; }
    public UserRoles Role { get; init; }
    public UserStatuses Status { get; init; }
    public BigInteger Balance { get; init; }

    public UserModel WithDecreasedBalance(BigInteger dec) => Touch<UserModel>()
        with
        {
            Balance = Balance - dec,
        };
    
    public UserModel WithIncreasedBalance(BigInteger inc) => Touch<UserModel>()
        with
        {
            Balance = Balance + inc,
        };
}