namespace PurchaseApi.Purchase;

public sealed class AlreadySoldException() : InvalidOperationException("Already sold");
public sealed class NotEnoughBalanceException() :  InvalidOperationException("Not enough balance");
public sealed class SellingToSelfException() : InvalidOperationException("Selling to self");