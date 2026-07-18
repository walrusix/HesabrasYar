using HesabrasYar.Wallet.Samples;
using Xunit;

namespace HesabrasYar.Wallet.EntityFrameworkCore.Applications;

[Collection(WalletTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<WalletEntityFrameworkCoreTestModule>
{

}
