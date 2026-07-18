using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Identity;

namespace HesabrasYar.Wallet.Identity;

/// <summary>
/// Event handler that listens for ABP Identity user creation events
/// and publishes our custom UserCreatedEvent
/// </summary>
public class IdentityUserCreatedEventHandler : IDistributedEventHandler<EntityCreatedEto<IdentityUserEto>>, ITransientDependency
{
    private readonly ILocalEventBus _localEventBus;

    public IdentityUserCreatedEventHandler(ILocalEventBus localEventBus)
    {
        _localEventBus = localEventBus;
    }

    public async Task HandleEventAsync(EntityCreatedEto<IdentityUserEto> eventData)
    {
        // Publish our local UserCreatedEvent
        var userCreatedEvent = new UserCreatedEvent(
            eventData.Entity.Id,
            eventData.Entity.UserName,
            eventData.Entity.Email
        );

        await _localEventBus.PublishAsync(userCreatedEvent);
    }
}
