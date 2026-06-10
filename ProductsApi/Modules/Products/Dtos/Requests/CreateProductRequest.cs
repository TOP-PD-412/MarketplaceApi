using System.ComponentModel.DataAnnotations;
using ProductsApi.Modules.Products.Domain.Models;

namespace ProductsApi.Modules.Products.Dtos.Requests;

public sealed record CreateProductRequest
{
    [Required] public required string Name { get; init; }

    public ProductModel ConvertToProduct() =>
        new()
        {
            Name = Name
        };
}