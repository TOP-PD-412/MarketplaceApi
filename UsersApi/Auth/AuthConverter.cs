using Shared.Petitions;
using Shared.Users;

namespace UsersAPI.Auth;

public static class AuthConverter
{
    public static UserModel ConvertToUser(this RegisterRequest request, UserRoles role) => new()
    {
        Name = request.Name,
        Phone = request.PhoneNumber,
        Role = role,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow,
    };

    public static CreateSellerPetitionModel ConvertToCreateSellerPetition(this UserModel user) => new()
    {
        Name = user.Name,
        Phone = user.Phone,
        PasswordHash = user.PasswordHash,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow,
    };
}