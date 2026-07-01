using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Petitions;
using Shared.Users;

namespace UsersAPI.Auth;

public sealed class AuthService(
    UsersRepo usersRepo,
    CreateSellerPetitionsRepo createSellerPetitionsRepo,
    PasswordHasher<UserModel> hasher,
    IOptions<JwtSettings> jwtOptions)
{
    private readonly JwtSettings _jwtSettings = jwtOptions.Value;

    public async Task<List<Claim>> RegisterBuyerAsync(RegisterRequest request)
    {
        var user = request.ConvertToUser(UserRoles.Buyer);
        user = user.WithPasswordHash(hasher.HashPassword(user, request.Password));
        try
        {
            await usersRepo.AddAsync(user);
        }
        catch
        {
            throw new InvalidOperationException("Phone number is already in use");
        }

        return
        [
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        ];
    }

    public async Task RegisterSellerAsync(RegisterRequest request)
    {
        if (await usersRepo.FindByPhoneNumber(request.PhoneNumber) != null)
            throw new InvalidOperationException("Phone number is already in use");

        var user = request.ConvertToUser(UserRoles.Seller);
        user = user.WithPasswordHash(hasher.HashPassword(user, request.Password));
        var petition = user.ConvertToCreateSellerPetition();
        await createSellerPetitionsRepo.AddAsync(petition);
    }

    public async Task<List<Claim>?> ValidateCredentials(LoginRequest request)
    {
        var user = await usersRepo.FindByPhoneNumber(request.PhoneNumber);
        if (user is not { Status: UserStatuses.Active }) return null;

        var verification = hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        if (verification == PasswordVerificationResult.Failed) return null;

        return
        [
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        ];
    }

    public AuthResponse GenerateJwtToken(List<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credentials
        );

        return new AuthResponse { Token = new JwtSecurityTokenHandler().WriteToken(token) };
    }
}