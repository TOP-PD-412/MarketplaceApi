using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using Shared.Constants;
using Shared.Infrastructure;
using Shared.Users;

namespace Shared.Purchases;

[Table("purchases")]
public sealed class PurchaseEntity : Entity<PurchaseEntity>
{
    [Required] [Column("seller_id")] public Guid SellerId { get; set; }
    [ForeignKey(nameof(SellerId))] public UserEntity? Seller { get; set; }

    [Required] [Column("buyer_id")] public Guid BuyerId { get; set; }
    [ForeignKey(nameof(BuyerId))] public UserEntity? Buyer { get; set; }

    [Required] [Column("price_paid")] public BigInteger PricePaid { get; set; }

    [Required]
    [Column("product_name")]
    [StringLength(Limits.Product.Name.MaxLength)]
    public string ProductName { get; set; } = string.Empty;
}