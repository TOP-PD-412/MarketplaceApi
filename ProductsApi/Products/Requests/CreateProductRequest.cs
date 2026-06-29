using System.ComponentModel.DataAnnotations;
using Shared.Constants;

namespace ProductsApi.Products.Requests;

public sealed record CreateProductRequest
{
    [Required]
    [MaxLength(Limits.Product.Name.MaxLength)]
    public required string Name { get; init; }
    
}