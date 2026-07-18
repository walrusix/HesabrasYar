using System.Threading.Tasks;
using HesabrasYar.Wallet.Permissions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Uow;

namespace HesabrasYar.Wallet.DbMigrator;

public class WalletPermissionDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IPermissionManager _permissionManager;

    public WalletPermissionDataSeedContributor(IPermissionManager permissionManager)
    {
        _permissionManager = permissionManager;
    }

    [UnitOfWork]
    public virtual async Task SeedAsync(DataSeedContext context)
    {
        await _permissionManager.SetForRoleAsync("WalletOwner", WalletPermissions.Wallets.Default, true);
        await _permissionManager.SetForRoleAsync("WalletOwner", WalletPermissions.Wallets.Create, true);
    }
}
