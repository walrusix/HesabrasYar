using System;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace HesabrasYar.Wallet.Wallets;

public class Transaction : Entity<Guid>, IHasCreationTime
{
    public virtual Guid WalletId { get; private set; }
    public virtual decimal Volume { get; private set; }
    public virtual TransactionEntryType Entry { get; private set; }
    public virtual DateTime CreationTime { get; set; }

    protected Transaction()
    {
        /* Required by EF Core */
    }

    internal Transaction(Guid id, Guid walletId,  decimal volume, TransactionEntryType entry)
        : base(id)
    {
        WalletId = walletId;
        Volume = volume;
        Entry = entry;
    }
}
