using System;
using System.ComponentModel.DataAnnotations;

namespace HesabrasYar.Wallet.Wallets;

public class CreateWalletDto
{
    [Required]
    [StringLength(WalletConsts.MaxNameLength)]
    public string Name { get; set; }

    [Required]
    public WalletType WalletType { get; set; }

    [Required]
    public CurrencyType Currency { get; set; }

    public Guid? ParentWalletId { get; set; }

    [Range(0, double.MaxValue)]
    public decimal InitialBalance { get; set; }
}
