using System;
using System.Linq;
using System.Threading.Tasks;
using HesabrasYar.Wallet.Wallets;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.Uow;

namespace HesabrasYar.Wallet.Identity;

public class IdentityRoleDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private const string SeedPassword = "123456aA@";

    private readonly IdentityRoleManager _roleManager;
    private readonly IdentityUserManager _userManager;
    private readonly IUserWalletCreationService _userWalletCreationService;

    public IdentityRoleDataSeedContributor(
        IdentityRoleManager roleManager,
        IdentityUserManager userManager,
        IUserWalletCreationService userWalletCreationService)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _userWalletCreationService = userWalletCreationService;
    }

    [UnitOfWork]
    public virtual async Task SeedAsync(DataSeedContext context)
    {
        await CreateRoleIfNotExistsAsync("admin", context?.TenantId);
        await CreateRoleIfNotExistsAsync("FinancialAdmin", context?.TenantId);
        await CreateRoleIfNotExistsAsync("WalletOwner", context?.TenantId);

        await SetPasswordAsync("admin", SeedPassword);

        await CreateUserIfNotExistsAsync("user1", SeedPassword, "WalletOwner");
        await CreateUserIfNotExistsAsync("user2", SeedPassword, "WalletOwner");
    }

    private async Task CreateRoleIfNotExistsAsync(string roleName, Guid? tenantId)
    {
        if (await _roleManager.FindByNameAsync(roleName) != null)
        {
            return;
        }

        var role = new IdentityRole(Guid.NewGuid(), roleName, tenantId)
        {
            IsStatic = false,
            IsPublic = true
        };

        var result = await _roleManager.CreateAsync(role);
        EnsureSucceeded(result, $"create role '{roleName}'");
    }

    private async Task SetPasswordAsync(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null)
        {
            return;
        }

        if (await _userManager.HasPasswordAsync(user))
        {
            EnsureSucceeded(await _userManager.RemovePasswordAsync(user), $"remove password for '{userName}'");
        }

        EnsureSucceeded(await _userManager.AddPasswordAsync(user, password), $"set password for '{userName}'");
    }

    private async Task CreateUserIfNotExistsAsync(string userName, string password, string roleName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null)
        {
            user = new IdentityUser(Guid.NewGuid(), userName, $"{userName}@wallet.local");
            EnsureSucceeded(await _userManager.CreateAsync(user, password), $"create user '{userName}'");
        }

        if (!await _userManager.IsInRoleAsync(user, roleName))
        {
            EnsureSucceeded(await _userManager.AddToRoleAsync(user, roleName), $"add '{userName}' to role '{roleName}'");
        }

        await _userWalletCreationService.EnsureDefaultWalletAsync(user.Id, user.UserName);
    }

    private static void EnsureSucceeded(Microsoft.AspNetCore.Identity.IdentityResult result, string action)
    {
        if (!result.Succeeded)
        {
            throw new AbpException($"Could not {action}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }
    }
}
