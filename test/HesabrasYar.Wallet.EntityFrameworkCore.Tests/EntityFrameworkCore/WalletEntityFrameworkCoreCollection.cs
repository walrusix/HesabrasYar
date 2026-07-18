using Xunit;

namespace HesabrasYar.Wallet.EntityFrameworkCore;

[CollectionDefinition(WalletTestConsts.CollectionDefinitionName)]
public class WalletEntityFrameworkCoreCollection : ICollectionFixture<WalletEntityFrameworkCoreFixture>
{

}
