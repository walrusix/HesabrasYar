using Volo.Abp.Modularity;

namespace HesabrasYar.Wallet;

/* Inherit from this class for your domain layer tests. */
public abstract class WalletDomainTestBase<TStartupModule> : WalletTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
