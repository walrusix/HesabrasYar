using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;

namespace HesabrasYar.Wallet.Wallets;

public class Wallet : FullAuditedAggregateRoot<Guid>, IHasCreationTime
{
    internal virtual int Code { get; private set; }

    public virtual string Name { get; private set; }

    public virtual WalletType WalletType { get; private set; }

    public virtual CurrencyType Currency { get; private set; }

    public virtual Guid OwnerId { get; private set; }

    public virtual Guid? ParentWalletId { get; private set; }

    public virtual decimal InitialBalance { get; private set; }

    public virtual bool IsActive { get; private set; }

    public virtual WalletBalance? Balance { get; private set; }

    public virtual ICollection<Transaction> Transactions { get; protected set; } = new List<Transaction>();

    public virtual string DisplayCode => $"W-{Code}";
    protected Wallet()
    {
        /* Required by EF Core */
    }

    internal Wallet(
        Guid id,
        [NotNull] string name,
        WalletType walletType,
        CurrencyType currency,
        Guid ownerId,
        decimal initialBalance,
        Guid? parentWalletId = null)
        : base(id)
    {
        SetName(name);
        WalletType = walletType;
        Currency = currency;
        OwnerId = ownerId;
        ParentWalletId = parentWalletId;
        InitialBalance = initialBalance;
        IsActive = true;
    }

    internal Wallet SetName([NotNull] string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), maxLength: WalletConsts.MaxNameLength);
        return this;
    }

    public void SetParent(Guid? parentWalletId)
    {
        if (parentWalletId.HasValue && parentWalletId.Value == Id)
        {
            throw new BusinessException(WalletDomainErrorCodes.WalletCannotBeItsOwnParent);
        }

        ParentWalletId = parentWalletId;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public virtual Transaction AddTransaction(Guid transactionId, decimal volume, TransactionEntryType entry)
    {
        var transaction = new Transaction(transactionId, Id, volume, entry);
        Transactions.Add(transaction);
        return transaction;
    }

    public virtual void InitializeBalance(Guid balanceId)
    {
        if (Balance != null)
        {
            throw new BusinessException(WalletDomainErrorCodes.WalletBalanceAlreadyInitialized);
        }

        Balance = new WalletBalance(balanceId, Id, 0m);
    }

    public virtual void ChangeBalance(decimal amount)
    {
        if (Balance == null)
        {
            throw new BusinessException(WalletDomainErrorCodes.WalletBalanceNotInitialized);
        }

        Balance.Change(amount);
    }


}
