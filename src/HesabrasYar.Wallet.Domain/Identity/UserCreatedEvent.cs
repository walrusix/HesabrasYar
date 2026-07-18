using System;

namespace HesabrasYar.Wallet.Identity;

/// <summary>
/// Event published when a new user is created in the system.
/// </summary>
public class UserCreatedEvent
{
    public Guid UserId { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string? Email { get; set; }

    public UserCreatedEvent()
    {
    }

    public UserCreatedEvent(Guid userId, string userName, string? email = null)
    {
        UserId = userId;
        UserName = userName;
        Email = email;
    }
}
