using System;
using Volo.Abp.Application.Dtos;

namespace HesabrasYar.Wallet.Wallets;

public class WalletDto : FullAuditedEntityDto<Guid>
{
    public string DisplayCode { get; set; }

    public string Name { get; set; }

    public WalletType WalletType { get; set; }

    public CurrencyType Currency { get; set; }

    public Guid OwnerId { get; set; }

    public Guid? ParentWalletId { get; set; }

    public decimal InitialBalance { get; set; }

    public decimal Balance { get; set; }

    public bool IsActive { get; set; }
}
