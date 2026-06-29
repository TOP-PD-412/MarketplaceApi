using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using Shared.Constants;
using Shared.Infrastructure;

namespace Shared.Users;

[Table("users")]
public sealed class UserEntity : Entity<UserEntity>
{
    [Required]
    [Column("name")]
    [StringLength(Limits.User.Name.MaxLength)]
    public string Name { get; set; } = string.Empty;

    [Required] [Column("phone")] public string Phone { get; set; } = string.Empty;
    [Column("password_hash")] public string? PasswordHash { get; set; }

    [Required]
    [Column("role", TypeName = "text")]
    public UserRoles Role { get; set; }

    [Required]
    [Column("status", TypeName = "text")]
    public UserStatuses Status { get; set; }

    [Required] [Column("balance")] public BigInteger Balance { get; set; }

    public override void Update(UserEntity other)
    {
        base.Update(other);
        Name = other.Name;
        Phone = other.Phone;
        PasswordHash = other.PasswordHash;
        Role = other.Role;
        Status = other.Status;
        Balance = other.Balance;
    }
}