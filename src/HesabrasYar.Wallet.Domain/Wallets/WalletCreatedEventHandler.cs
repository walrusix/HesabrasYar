using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.Guids;
using Volo.Abp.Uow;

namespace HesabrasYar.Wallet.Wallets;

public class WalletCreatedEventHandler : ILocalEventHandler<WalletCreatedEto>, ITransientDependency
{
    private readonly IWalletRepository _walletRepository;
    private readonly IGuidGenerator _guidGenerator;

    public WalletCreatedEventHandler(IWalletRepository walletRepository, IGuidGenerator guidGenerator)
    {
        _walletRepository = walletRepository;
        _guidGenerator = guidGenerator;
    }

    [UnitOfWork]
    public virtual async Task HandleEventAsync(WalletCreatedEto eventData)
    {
        var wallet = await _walletRepository.GetAsync(eventData.WalletId);

        wallet.InitializeBalance(_guidGenerator.Create());
        wallet.AddTransaction(_guidGenerator.Create(), eventData.InitialBalance, TransactionEntryType.InitialCharge);
        wallet.ChangeBalance(eventData.InitialBalance);

        await _walletRepository.UpdateAsync(wallet);
    }
}
