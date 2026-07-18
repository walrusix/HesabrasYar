using Microsoft.Extensions.Localization;
using HesabrasYar.Wallet.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace HesabrasYar.Wallet;

[Dependency(ReplaceServices = true)]
public class WalletBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<WalletResource> _localizer;

    public WalletBrandingProvider(IStringLocalizer<WalletResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
