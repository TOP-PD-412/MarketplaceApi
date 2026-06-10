using System.ComponentModel.DataAnnotations;
using ProductsApi.Modules.Products.Domain.Models;

namespace ProductsApi.Modules.Products.Dtos.Requests;

public sealed record UpdateProductRequest
{
    [Required] public required string Name { get; init; }

    public ProductModel ConvertToProduct(Guid id) =>
        new()
        {
            Id = id,
            Name = Name
        };
}