using Shared.Infrastructure;

namespace Shared.Petitions;

public abstract class PetitionsRepo<TPetitionModel>(AppDbContext ctx, PetitionMapper<TPetitionModel> mapper)
    : Repo<TPetitionModel, PetitionEntity>(ctx, ctx.Petitions, mapper) where TPetitionModel : PetitionModel, new();

public sealed class CreateSellerPetitionsRepo(AppDbContext ctx, CreateSellerPetitionMapper mapper)
    : PetitionsRepo<CreateSellerPetitionModel>(ctx, mapper);

public sealed class CreateProductPetitionsRepo(AppDbContext ctx, CreateProductPetitionMapper mapper)
    : PetitionsRepo<CreateProductPetitionModel>(ctx, mapper);