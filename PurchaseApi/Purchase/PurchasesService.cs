using Shared.Infrastructure;
using Shared.Products;
using Shared.Purchases;
using Shared.Users;

namespace PurchaseApi.Purchase;

public sealed class PurchasesService(
    TransactionManager transactionManager,
    PurchasesRepo purchasesRepo,
    ProductsRepo productsRepo,
    UsersRepo usersRepo)
{
    public async Task<CreatePurchaseResponse> CretePurchaseAsync(CreatePurchaseRequest request, Guid buyerId)
    {
        await using var transaction = await transactionManager.BeginTransactionAsync();
        try
        {
            var product = await productsRepo.FindByIdAndLockAsync(request.ProductId);
            if (product is null) throw new KeyNotFoundException("Product not found");
            if (product.Amount == 0) throw new AlreadySoldException();

            var buyer = await usersRepo.FindByIdAndLockAsync(buyerId);
            if (buyer is null) throw new KeyNotFoundException("Buyer not found");
            if (buyer.Balance < product.Price) throw new NotEnoughBalanceException();

            var seller = await usersRepo.FindByIdAndLockAsync(product.SellerId);
            if (seller is null) throw new KeyNotFoundException("Seller not found");
            if (seller.Id == buyer.Id) throw new SellingToSelfException();

            buyer = buyer.WithDecreasedBalance(product.Price);
            seller = seller.WithIncreasedBalance(product.Price);
            product = product.WithDecreasedAmount();
            var purchase = new PurchaseModel
            {
                BuyerId = buyer.Id,
                SellerId = seller.Id,
                ProductName = product.Name,
                PricePaid = product.Price,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await usersRepo.UpdateAsync(buyer);
            await usersRepo.UpdateAsync(seller);
            await productsRepo.UpdateAsync(product);
            await purchasesRepo.AddAsync(purchase);

            await transaction.CommitAsync();
            return purchase.ConvertToCreatePurchaseResponse();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<GetPurchaseResponse?> GetPurchaseAsync(Guid id)
    {
        var purchase = await purchasesRepo.FindByIdAsync(id);
        return purchase?.ConvertToGetPurchaseResponse();
    }
}