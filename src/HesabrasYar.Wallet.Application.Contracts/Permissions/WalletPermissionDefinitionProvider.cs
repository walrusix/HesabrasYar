using HesabrasYar.Wallet.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace HesabrasYar.Wallet.Permissions;

public class WalletPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(WalletPermissions.GroupName, L("Permission:Wallet"));

        var walletsPermission = myGroup.AddPermission(WalletPermissions.Wallets.Default, L("Permission:Wallets"));
        walletsPermission.AddChild(WalletPermissions.Wallets.Create, L("Permission:Wallets.Create"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<WalletResource>(name);
    }
}
