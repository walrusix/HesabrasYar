using System.Collections.Generic;

namespace HesabrasYar.Wallet.Wallets;

public class MyWalletsDto
{
    public List<WalletDto> Wallets { get; set; }

    public decimal TotalBalance { get; set; }
}
