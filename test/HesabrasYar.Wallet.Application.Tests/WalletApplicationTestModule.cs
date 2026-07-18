using Volo.Abp.Modularity;

namespace HesabrasYar.Wallet;

[DependsOn(
    typeof(WalletApplicationModule),
    typeof(WalletDomainTestModule)
)]
public class WalletApplicationTestModule : AbpModule
{

}
