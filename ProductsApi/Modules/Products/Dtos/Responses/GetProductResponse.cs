using System.ComponentModel.DataAnnotations;
using ProductsApi.Modules.Products.Domain.Models;

namespace ProductsApi.Modules.Products.Dtos.Responses;

public sealed record GetProductResponse
{
    [Required] public required Guid Id { get; init; }
    [Required] public string Name { get; init; } = string.Empty;

    public static GetProductResponse CreateFrom(ProductModel product) => new()
    {
        Id = product.Id,
        Name = product.Name
    };
}