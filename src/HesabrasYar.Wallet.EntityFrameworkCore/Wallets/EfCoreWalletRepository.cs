using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HesabrasYar.Wallet.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HesabrasYar.Wallet.Wallets;

public class EfCoreWalletRepository : EfCoreRepository<WalletDbContext, Wallet, Guid>, IWalletRepository
{
    public EfCoreWalletRepository(IDbContextProvider<WalletDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<Wallet> FindByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(
            w => w.Name == name,
            GetCancellationToken(cancellationToken)
        );
    }

    public async Task<bool> NameExistsAsync(string name, Guid? excludeId = null, CancellationToken cancellationToken = default)
    {
        var dbSet = await GetDbSetAsync();
        var query = dbSet.Where(w => w.Name == name);

        if (excludeId.HasValue)
        {
            query = query.Where(w => w.Id != excludeId.Value);
        }

        return await query.AnyAsync(GetCancellationToken(cancellationToken));
    }

    public async Task<List<Wallet>> GetListByOwnerAsync(Guid ownerId, CancellationToken cancellationToken = default)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet
            .Where(w => w.OwnerId == ownerId)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }
}
