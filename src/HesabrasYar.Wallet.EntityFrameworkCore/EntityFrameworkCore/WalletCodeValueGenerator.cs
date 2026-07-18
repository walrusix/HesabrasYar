using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Volo.Abp.EntityFrameworkCore;

namespace HesabrasYar.Wallet.EntityFrameworkCore;

public class WalletCodeValueGenerator : ValueGenerator<int>
{
    public override bool GeneratesTemporaryValues => false;

    public override int Next(EntityEntry entry)
    {
        var context = (WalletDbContext)entry.Context;

        var maxCode = context.Wallets
            .Select(w => (int?)w.Code)
            .Max() ?? 10000;

        return maxCode + 1;
    }
}
