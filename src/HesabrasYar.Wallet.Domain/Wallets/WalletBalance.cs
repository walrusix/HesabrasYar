using System;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace HesabrasYar.Wallet.Wallets;

public class WalletBalance : Entity<Guid>, IHasCreationTime
{
    public virtual Guid WalletId { get; private set; }

    public virtual decimal Balance { get; private set; }

    public virtual DateTime CreationTime { get; set; }

    protected WalletBalance()
    {
        /* Required by EF Core */
    }

    internal WalletBalance(Guid id, Guid walletId, decimal balance)
        : base(id)
    {
        WalletId = walletId;
        Balance = balance;
    }

    internal void Change(decimal amount)
    {
        if(Balance+amount < 0)  throw new BusinessException(WalletDomainErrorCodes.InsufficientWalletBalance);
        Balance += amount;
    }

}
