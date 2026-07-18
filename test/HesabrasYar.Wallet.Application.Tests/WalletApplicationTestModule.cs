using HesabrasYar.Wallet.Wallets;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace HesabrasYar.Wallet;

[DependsOn(
    typeof(WalletApplicationModule),
    typeof(WalletDomainTestModule)
)]
public class WalletApplicationTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient<IWalletAppService, WalletsAppService>();
    }
}
