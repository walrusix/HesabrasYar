using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;
using Volo.Abp.Guids;

namespace HesabrasYar.Wallet.Wallets;

public class WalletManager : DomainService
{
    private readonly IWalletRepository _walletRepository;
    private readonly IGuidGenerator _guidGenerator;
    private readonly ILocalEventBus _localEventBus;

    public WalletManager(IWalletRepository walletRepository, IGuidGenerator guidGenerator, ILocalEventBus localEventBus)
    {
        _walletRepository = walletRepository;
        _guidGenerator = guidGenerator;
        _localEventBus = localEventBus;
    }

    public async Task<Wallet> CreateAsync(
        string name,
        WalletType walletType,
        CurrencyType currency,
        Guid ownerId,
        decimal initialBalance,
        Guid? parentWalletId = null)
    {
        await CheckNameIsUniqueAsync(name);

        if (parentWalletId.HasValue)
        {
            await CheckParentBelongsToOwnerAsync(parentWalletId.Value, ownerId);
        }

        var wallet = new Wallet(
            _guidGenerator.Create(),
            name,
            walletType,
            currency,
            ownerId,
            initialBalance,
            parentWalletId
        );

        await _walletRepository.InsertAsync(wallet);

        await _localEventBus.PublishAsync(new WalletCreatedEto(wallet.Id, wallet.OwnerId, wallet.InitialBalance));

        return wallet;
    }

    public async Task SetNameAsync(Wallet wallet, string name)
    {
        if (wallet.Name == name)
        {
            return;
        }

        await CheckNameIsUniqueAsync(name, wallet.Id);
        wallet.SetName(name);
    }

    public async Task SetParentAsync(Wallet wallet, Guid? parentWalletId)
    {
        if (parentWalletId.HasValue)
        {
            await CheckParentBelongsToOwnerAsync(parentWalletId.Value, wallet.OwnerId);
        }

        wallet.SetParent(parentWalletId);
    }

    private async Task CheckNameIsUniqueAsync(string name, Guid? excludeId = null)
    {
        if (await _walletRepository.NameExistsAsync(name, excludeId))
        {
            throw new BusinessException(WalletDomainErrorCodes.DuplicateWalletName)
                .WithData("name", name);
        }
    }

    private async Task CheckParentBelongsToOwnerAsync(Guid parentWalletId, Guid ownerId)
    {
        var parentWallet = await _walletRepository.FindAsync(parentWalletId);
        if (parentWallet == null)
        {
            throw new BusinessException(WalletDomainErrorCodes.ParentWalletNotFound)
                .WithData("parentWalletId", parentWalletId);
        }

        if (parentWallet.OwnerId != ownerId)
        {
            throw new BusinessException(WalletDomainErrorCodes.ParentWalletOwnerMismatch)
                .WithData("parentWalletId", parentWalletId);
        }
    }
}
