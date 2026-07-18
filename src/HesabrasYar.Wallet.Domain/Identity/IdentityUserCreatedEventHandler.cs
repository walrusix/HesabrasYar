using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;
using Volo.Abp.EventBus.Local;
using Volo.Abp.Identity;

namespace HesabrasYar.Wallet.Identity;

/// <summary>
/// Event handler that listens for ABP Identity user creation domain events
/// and publishes our custom UserCreatedEvent to trigger wallet creation
/// </summary>
public class IdentityUserCreatedEventHandler : ILocalEventHandler<EntityCreatedEventData<IdentityUser>>, ITransientDependency
{
    private readonly ILocalEventBus _localEventBus;

    public IdentityUserCreatedEventHandler(ILocalEventBus localEventBus)
    {
        _localEventBus = localEventBus;
    }

    public async Task HandleEventAsync(EntityCreatedEventData<IdentityUser> eventData)
    {
        // Publish our local UserCreatedEvent when ABP creates a user
        var userCreatedEvent = new UserCreatedEvent(
            eventData.Entity.Id,
            eventData.Entity.UserName,
            eventData.Entity.Email
        );

        await _localEventBus.PublishAsync(userCreatedEvent);
    }
}
