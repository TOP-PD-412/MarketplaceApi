using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Infrastructure;

namespace Shared.Petitions;

[Table("petitions")]
public sealed class PetitionEntity : Entity<PetitionEntity>
{
    [Required]
    [Column("type", TypeName = "text")]
    public PetitionType Type { get; set; }

    [Required]
    [Column("status", TypeName = "text")]
    public PetitionStatus Status { get; set; }

    [Required]
    [Column("payload", TypeName = "jsonb")]
    public Dictionary<string, string?> Payload { get; set; } = [];
}
