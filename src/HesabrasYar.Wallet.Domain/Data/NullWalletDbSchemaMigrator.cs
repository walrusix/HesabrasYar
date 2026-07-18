using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace HesabrasYar.Wallet.Data;

/* This is used if database provider does't define
 * IWalletDbSchemaMigrator implementation.
 */
public class NullWalletDbSchemaMigrator : IWalletDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
