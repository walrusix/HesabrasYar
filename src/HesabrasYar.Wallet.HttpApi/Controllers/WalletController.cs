using HesabrasYar.Wallet.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace HesabrasYar.Wallet.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class WalletController : AbpControllerBase
{
    protected WalletController()
    {
        LocalizationResource = typeof(WalletResource);
    }
}
