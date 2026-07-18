using System;

namespace HesabrasYar.Wallet.Wallets;

public class WalletCreatedEto
{
    public Guid WalletId { get; set; }

    public Guid OwnerId { get; set; }

    public decimal InitialBalance { get; set; }

    public WalletCreatedEto()
    {
    }

    public WalletCreatedEto(Guid walletId, Guid ownerId, decimal initialBalance)
    {
        WalletId = walletId;
        OwnerId = ownerId;
        InitialBalance = initialBalance;
    }
}
