using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HesabrasYar.Wallet.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Users;

namespace HesabrasYar.Wallet.Wallets;

[Authorize(WalletPermissions.Wallets.Default)]
public class WalletsAppService : WalletAppService, IWalletAppService
{
    private readonly WalletManager _walletManager;
    private readonly IWalletRepository _walletRepository;

    public WalletsAppService(WalletManager walletManager, IWalletRepository walletRepository)
    {
        _walletManager = walletManager;
        _walletRepository = walletRepository;
    }

    [Authorize(WalletPermissions.Wallets.Create)]
    public async Task<WalletDto> CreateAsync(CreateWalletDto input)
    {
        var wallet = await _walletManager.CreateAsync(
            input.Name,
            input.WalletType,
            input.Currency,
            CurrentUser.GetId(),
            input.InitialBalance,
            input.ParentWalletId
        );

        return ObjectMapper.Map<Wallet, WalletDto>(wallet);
    }

    public async Task<MyWalletsDto> GetMyWalletsAsync()
    {
        var wallets = await _walletRepository.GetListByOwnerAsync(CurrentUser.GetId());

        var walletDtos = ObjectMapper.Map<List<Wallet>, List<WalletDto>>(wallets);

        return new MyWalletsDto
        {
            Wallets = walletDtos,
            TotalBalance = walletDtos.Sum(w => w.Balance)
        };
    }
}
