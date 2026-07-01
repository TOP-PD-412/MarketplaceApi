using System.Collections.Frozen;
using System.Numerics;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure;
using Shared.Utils;

namespace Shared.Petitions;

file static class PetitionPayloadFields
{
    public const string Name = "name";
    public const string Phone = "phone";
    public const string PasswordHash = "passwordHash";

    public const string Description = "description";
    public const string ImageUrls = "imageUrls";
    public const string Price = "price";
    public const string Amount = "amount";
    public const string SellerId = "sellerId";
    public const string Characteristics = "characteristics";

    public const string BuyerId = "buyerId";
    public const string ProductId = "productId";
    public const string Text = "text";
    public const string Rating = "rating";
}

public abstract class PetitionMapper<TPetitionModel> : Mapper<TPetitionModel, PetitionEntity>
    where TPetitionModel : PetitionModel, new()
{
    public override PetitionEntity MapToEntity(TPetitionModel model)
    {
        var entity = base.MapToEntity(model);
        entity.Type = model.Type;
        entity.Status = model.Status;
        return entity;
    }

    public override TPetitionModel MapToModel(PetitionEntity entity)
    {
        return base.MapToModel(entity) with
        {
            Type = entity.Type,
            Status = entity.Status,
        };
    }
}

public sealed class CreateSellerPetitionMapper : PetitionMapper<CreateSellerPetitionModel>
{
    public override PetitionEntity MapToEntity(CreateSellerPetitionModel model)
    {
        var entity = base.MapToEntity(model);
        entity.Payload = new Dictionary<string, string?>
        {
            [PetitionPayloadFields.Name] = model.Name,
            [PetitionPayloadFields.Phone] = model.Phone,
            [PetitionPayloadFields.PasswordHash] = model.PasswordHash,
        };
        return entity;
    }

    public override CreateSellerPetitionModel MapToModel(PetitionEntity entity)
    {
        return base.MapToModel(entity) with
        {
            Name = entity.Payload[PetitionPayloadFields.Name]!,
            Phone = entity.Payload[PetitionPayloadFields.Phone]!,
            PasswordHash = entity.Payload[PetitionPayloadFields.PasswordHash]!
        };
    }
}

public sealed class CreateProductPetitionMapper : PetitionMapper<CreateProductPetitionModel>
{
    public override PetitionEntity MapToEntity(CreateProductPetitionModel model)
    {
        var entity = base.MapToEntity(model);
        entity.Payload = new Dictionary<string, string?>
        {
            [PetitionPayloadFields.Name] = model.Name,
            [PetitionPayloadFields.Description] = model.Description,
            [PetitionPayloadFields.ImageUrls] = JsonSerializer.Serialize(model.ImageUrls.Select(url => url.ToString())),
            [PetitionPayloadFields.Price] = model.Price.ToString(),
            [PetitionPayloadFields.Amount] = model.Amount.ToString(),
            [PetitionPayloadFields.SellerId] = model.SellerId.ToString(),
            [PetitionPayloadFields.Characteristics] = JsonSerializer.Serialize(model.Characteristics),
        };
        return entity;
    }

    public override CreateProductPetitionModel MapToModel(PetitionEntity entity)
    {
        return base.MapToModel(entity) with
        {
            Name = entity.Payload[PetitionPayloadFields.Name]!,
            Description = entity.Payload[PetitionPayloadFields.Name],
            ImageUrls = JsonSerializer.Deserialize<string[]>(entity.Payload[PetitionPayloadFields.ImageUrls]!)!
                .Select(str => str.ToUri()!)
                .ToArray(),
            Price = BigInteger.Parse(entity.Payload[PetitionPayloadFields.Price]!),
            Amount = int.Parse(entity.Payload[PetitionPayloadFields.Amount]!),
            SellerId = Guid.Parse(entity.Payload[PetitionPayloadFields.SellerId]!),
            Characteristics =
            JsonSerializer.Deserialize<Dictionary<string, string>>(
                    entity.Payload[PetitionPayloadFields.Characteristics]!)!
                .ToFrozenDictionary()
        };
    }
}

