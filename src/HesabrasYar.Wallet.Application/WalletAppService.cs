using System;
using System.Collections.Generic;
using System.Text;
using HesabrasYar.Wallet.Localization;
using Volo.Abp.Application.Services;

namespace HesabrasYar.Wallet;

/* Inherit your application services from this class.
 */
public abstract class WalletAppService : ApplicationService
{
    protected WalletAppService()
    {
        LocalizationResource = typeof(WalletResource);
    }
}
