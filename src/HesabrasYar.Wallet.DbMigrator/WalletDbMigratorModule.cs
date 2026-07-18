using HesabrasYar.Wallet.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace HesabrasYar.Wallet.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(WalletEntityFrameworkCoreModule),
    typeof(WalletApplicationContractsModule)
    )]
public class WalletDbMigratorModule : AbpModule
{
}
