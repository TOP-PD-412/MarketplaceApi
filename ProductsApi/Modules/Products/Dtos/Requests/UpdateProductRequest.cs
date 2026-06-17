using System.ComponentModel.DataAnnotations;
using ProductsApi.Core.Constants;

namespace ProductsApi.Modules.Products.Dtos.Requests;

public sealed record UpdateProductRequest
{
    [Required]
    [MaxLength(Limits.Product.Name.MaxLength)]
    public required string Name { get; init; }

    public string? PreviewUrl { get; init; }
}