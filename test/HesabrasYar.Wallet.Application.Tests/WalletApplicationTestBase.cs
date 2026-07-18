using Volo.Abp.Modularity;

namespace HesabrasYar.Wallet;

public abstract class WalletApplicationTestBase<TStartupModule> : WalletTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
