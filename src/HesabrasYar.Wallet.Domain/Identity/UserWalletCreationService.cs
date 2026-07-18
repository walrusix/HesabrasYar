using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using HesabrasYar.Wallet.Wallets;

namespace HesabrasYar.Wallet.Identity;

/// <summary>
/// Implementation of IUserWalletCreationService.
/// Automatically creates a wallet for users when they are registered.
/// </summary>
public class UserWalletCreationService : IUserWalletCreationService, ITransientDependency
{
    private readonly WalletManager _walletManager;
    private readonly IWalletRepository _walletRepository;

    public UserWalletCreationService(WalletManager walletManager, IWalletRepository walletRepository)
    {
        _walletManager = walletManager;
        _walletRepository = walletRepository;
    }

    public async Task EnsureDefaultWalletAsync(Guid userId, string userName)
    {
        // Check if user already has a wallet
        var existingWallet = await _walletRepository.FindAsync(w => w.OwnerId == userId);
        
        if (existingWallet != null)
        {
            // User already has a wallet, no need to create another one
            return;
        }

        // Create a default wallet for the user with initial balance of 0
        var walletName = $"My Wallet - {userName}";
        
        await _walletManager.CreateAsync(
            name: walletName,
            walletType: WalletType.Personal,
            currency: CurrencyType.IRR,
            ownerId: userId,
            initialBalance: 0m
        );
    }
}
