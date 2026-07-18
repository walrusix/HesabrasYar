using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace HesabrasYar.Wallet.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class WalletDbContext :
    AbpDbContext<WalletDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }
    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public DbSet<global::HesabrasYar.Wallet.Wallets.Wallet> Wallets { get; set; }

    public DbSet<global::HesabrasYar.Wallet.Wallets.WalletBalance> WalletBalances { get; set; }

    public DbSet<global::HesabrasYar.Wallet.Wallets.Transaction> Transactions { get; set; }

    public WalletDbContext(DbContextOptions<WalletDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        builder.Entity<global::HesabrasYar.Wallet.Wallets.Wallet>(b =>
        {
            b.ToTable(WalletConsts.DbTablePrefix + "Wallets", WalletConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props

            if (Database.ProviderName?.Contains("Sqlite") == true)
            {
                b.Property(x => x.Code)
                    .HasValueGenerator<WalletCodeValueGenerator>()
                    .ValueGeneratedOnAdd()
                    .IsRequired();
            }
            else
            {
                b.Property(x => x.Code)
                    .UseIdentityColumn(10001, 1)
                    .IsRequired();
            }
            b.HasIndex(x => x.Code).IsUnique();

            b.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(global::HesabrasYar.Wallet.Wallets.WalletConsts.MaxNameLength);
            b.HasIndex(x => x.Name).IsUnique();

            b.Property(x => x.WalletType)
                .HasConversion<string>()
                .HasMaxLength(16)
                .IsRequired();

            b.Property(x => x.Currency)
                .HasConversion<string>()
                .HasMaxLength(16)
                .IsRequired();

            b.Property(x => x.InitialBalance)
                .HasColumnType("decimal(18,2)");

            b.HasOne<IdentityUser>()
                .WithMany()
                .HasForeignKey(x => x.OwnerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne<global::HesabrasYar.Wallet.Wallets.Wallet>()
                .WithMany()
                .HasForeignKey(x => x.ParentWalletId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(x => x.Balance)
                .WithOne()
                .HasForeignKey<global::HesabrasYar.Wallet.Wallets.WalletBalance>(x => x.WalletId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            b.Navigation(x => x.Balance).AutoInclude();

            b.HasMany(x => x.Transactions)
                .WithOne()
                .HasForeignKey(x => x.WalletId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<global::HesabrasYar.Wallet.Wallets.WalletBalance>(b =>
        {
            b.ToTable(WalletConsts.DbTablePrefix + "WalletBalances", WalletConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props

            b.Property(x => x.Balance)
                .HasColumnType("decimal(18,2)");

            b.HasIndex(x => x.WalletId).IsUnique();
        });

        builder.Entity<global::HesabrasYar.Wallet.Wallets.Transaction>(b =>
        {
            b.ToTable(WalletConsts.DbTablePrefix + "Transactions", WalletConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props

            b.Property(x => x.Entry)
                .HasConversion<string>()
                .HasMaxLength(16)
                .IsRequired();

            b.Property(x => x.Volume)
                .HasColumnType("decimal(18,2)");

            b.HasIndex(x => x.WalletId);
        });
    }
}
