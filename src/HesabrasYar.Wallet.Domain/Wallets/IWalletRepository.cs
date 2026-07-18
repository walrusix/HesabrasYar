using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HesabrasYar.Wallet.Wallets;

public interface IWalletRepository : IRepository<Wallet, Guid>
{
    Task<Wallet> FindByNameAsync(string name, CancellationToken cancellationToken = default);

    Task<bool> NameExistsAsync(string name, Guid? excludeId = null, CancellationToken cancellationToken = default);

    Task<List<Wallet>> GetListByOwnerAsync(Guid ownerId, CancellationToken cancellationToken = default);
}
