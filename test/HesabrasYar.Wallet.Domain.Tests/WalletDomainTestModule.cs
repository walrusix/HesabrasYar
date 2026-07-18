using Volo.Abp.Modularity;

namespace HesabrasYar.Wallet;

[DependsOn(
    typeof(WalletDomainModule),
    typeof(WalletTestBaseModule)
)]
public class WalletDomainTestModule : AbpModule
{

}
