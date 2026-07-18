using System;
using System.Threading.Tasks;
using HesabrasYar.Wallet.Wallets;
using HesabrasYar.Wallet.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Shouldly;
using Volo.Abp.Identity;
using Xunit;

namespace HesabrasYar.Wallet.WalletCreation.Tests;

public class WalletCreationTests : WalletEntityFrameworkCoreTestBase
{
    [Fact]
    public async Task Should_Create_Wallet_Successfully()
    {
        var walletManager = GetRequiredService<WalletManager>();
        var userManager = GetRequiredService<IdentityUserManager>();

        var ownerId = Guid.NewGuid();
        var ownerUser = new IdentityUser(ownerId, "test-user"+Random.Shared.NextDouble().ToString(), "test-user@wallet.local");

        var createUserResult = await userManager.CreateAsync(ownerUser, "123456aA@");
        createUserResult.Succeeded.ShouldBeTrue();

        var wallet = await walletManager.CreateAsync(
            "Integration Test Wallet",
            WalletType.Personal,
            CurrencyType.IRR,
            ownerId,
            1000m
        );

        wallet.ShouldNotBeNull();
        wallet.Name.ShouldBe("Integration Test Wallet");
        wallet.InitialBalance.ShouldBe(1000m);
        wallet.OwnerId.ShouldBe(ownerId);
        wallet.IsActive.ShouldBeTrue();
        wallet.ParentWalletId.ShouldBeNull();
        (await walletManager.GetWalletBalanceAsync(wallet.Id)).ShouldBe(1000m);
    }
}
