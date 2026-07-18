using System.Threading.Tasks;

namespace HesabrasYar.Wallet.Data;

public interface IWalletDbSchemaMigrator
{
    Task MigrateAsync();
}
