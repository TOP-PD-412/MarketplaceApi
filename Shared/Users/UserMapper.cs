using Shared.Infrastructure;

namespace Shared.Users;

public sealed class UserMapper : Mapper<UserModel, UserEntity>
{
    public override UserEntity MapToEntity(UserModel model)
    {
        var entity = base.MapToEntity(model);
        entity.Name = model.Name;
        entity.Phone = model.Phone;
        entity.PasswordHash = model.PasswordHash;
        entity.Role = model.Role;
        entity.Status = model.Status;
        entity.Balance = model.Balance;
        return entity;
    }

    public override UserModel MapToModel(UserEntity entity) =>
        base.MapToModel(entity) with
        {
            Name = entity.Name,
            Phone = entity.Phone,
            PasswordHash = entity.PasswordHash,
            Role = entity.Role,
            Status = entity.Status,
            Balance = entity.Balance,
        };
}