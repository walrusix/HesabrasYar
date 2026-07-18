using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace HesabrasYar.Wallet.Wallets;

public interface IWalletAppService : IApplicationService
{
    Task<WalletDto> CreateAsync(CreateWalletDto input);

    Task<MyWalletsDto> GetMyWalletsAsync();
}
