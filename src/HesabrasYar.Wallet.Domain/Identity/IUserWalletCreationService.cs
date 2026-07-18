using System;
using System.Threading.Tasks;

namespace HesabrasYar.Wallet.Identity;

/// <summary>
/// Service for managing wallet creation and initialization for users.
/// </summary>
public interface IUserWalletCreationService
{
    /// <summary>
    /// Ensures a default wallet is created for the given user.
    /// If a wallet already exists for the user, no new wallet is created.
    /// </summary>
    Task EnsureDefaultWalletAsync(Guid userId, string userName);
}
