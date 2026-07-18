using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HesabrasYar.Wallet.Data;
using Volo.Abp.DependencyInjection;

namespace HesabrasYar.Wallet.EntityFrameworkCore;

public class EntityFrameworkCoreWalletDbSchemaMigrator
    : IWalletDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreWalletDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the WalletDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<WalletDbContext>()
            .Database
            .MigrateAsync();
    }
}
