using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;

namespace HesabrasYar.Wallet.Identity;

/// <summary>
/// Event handler that automatically creates a wallet for newly registered users with initial balance of 0.
/// </summary>
public class UserCreatedEventHandler : ILocalEventHandler<UserCreatedEvent>, ITransientDependency
{
    private readonly IUserWalletCreationService _userWalletCreationService;

    public UserCreatedEventHandler(IUserWalletCreationService userWalletCreationService)
    {
        _userWalletCreationService = userWalletCreationService;
    }

    public async Task HandleEventAsync(UserCreatedEvent eventData)
    {
        // Create a default wallet for the newly registered user with initial balance of 0
        try
        {
            await _userWalletCreationService.EnsureDefaultWalletAsync(eventData.UserId, eventData.UserName);
        }
        catch (Exception ex)
        {
            // Log the error but don't throw - user registration should not fail if wallet creation fails
            Console.WriteLine($"Error creating wallet for user {eventData.UserId}: {ex.Message}");
        }
    }
}
