using Volo.Abp.Settings;

namespace HesabrasYar.Wallet.Settings;

public class WalletSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(WalletSettings.MySetting1));
    }
}
