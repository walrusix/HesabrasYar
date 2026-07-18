using HesabrasYar.Wallet.Samples;
using Xunit;

namespace HesabrasYar.Wallet.EntityFrameworkCore.Domains;

[Collection(WalletTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<WalletEntityFrameworkCoreTestModule>
{

}
