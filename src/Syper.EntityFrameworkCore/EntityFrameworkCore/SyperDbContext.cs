using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Syper.EntityFrameworkCore;

using Syper.Clients;
using Syper.Sets;
using Syper.Workouts;
using Syper.WorkoutSections;
using Syper.WorkoutExercises;
using Syper.Exercises;
using Syper.ClientCoachSubscriptions;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class SyperDbContext :
    AbpDbContext<SyperDbContext>,
    ITenantManagementDbContext,
    IIdentityDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    public DbSet<Client> Clients { get; set; }
    public DbSet<Workout> Workouts { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<ClientCoachSubscription> ClientCoachSubscription { get; set; }

    #region Entities from the modules

    /* Notice: We only implemented IIdentityProDbContext and ISaasDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityProDbContext and ISaasDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    // Identity
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

    public SyperDbContext(DbContextOptions<SyperDbContext> options)
        : base(options)
    {
        // options.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureFeatureManagement();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureTenantManagement();
        builder.ConfigureBlobStoring();

        builder.Entity<Client>(b =>
        {
            b.ToTable(SyperConsts.DbTablePrefix + "Clients",
                SyperConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.FirstName).IsRequired().HasMaxLength(32);
            b.Property(x => x.LastName).IsRequired().HasMaxLength(32);
            b.Property(x => x.Email).IsRequired().HasMaxLength(128);
            b.Property(x => x.ClientState).IsRequired();
            b.HasIndex(x => x.Email).IsUnique();
        });

        builder.Entity<Exercise>(b =>
        {
            b.ToTable(SyperConsts.DbTablePrefix + "Exercises",
                SyperConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.Title).IsRequired().HasMaxLength(32);
            b.Property(x => x.ExerciseCategory).IsRequired().HasMaxLength(32);
        });

        builder.Entity<Set>(b =>
        {
            b.ToTable(SyperConsts.DbTablePrefix + "Sets",
                SyperConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.Unit).IsRequired();
            b.Property(x => x.UnitType).IsRequired();
            b.Property(x => x.Quantity).IsRequired();
            b.Property(x => x.QuantityType).IsRequired();
            b.Property(x => x.Rest).IsRequired(false);
            b.Property(x => x.UpperPercentageOfMax).IsRequired(false);
            b.Property(x => x.PerceivedEffort).IsRequired(false);
        });

        builder.Entity<Workout>(b =>
        {
            b.ToTable(SyperConsts.DbTablePrefix + "Workouts",
                SyperConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.Name).IsRequired().HasMaxLength(32);
            b.Property(x => x.ShortDescription).HasMaxLength(255);
            b.HasMany(x => x.WorkoutSections).WithOne().HasForeignKey(x => x.WorkoutId).IsRequired();
        });

        builder.Entity<WorkoutSection>(b =>
        {
            b.ToTable(SyperConsts.DbTablePrefix + "WorkoutSections",
                SyperConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.Title).IsRequired().HasMaxLength(32);
            b.Property(x => x.Colour).IsRequired().HasMaxLength(32);
            b.Property(x => x.WorkoutId).IsRequired();
            b.HasOne(x => x.Workout).WithMany(x => x.WorkoutSections).HasForeignKey(x => x.WorkoutId).IsRequired();
            b.HasMany(x => x.WorkoutExercises).WithOne().HasForeignKey(x => x.WorkoutSectionId).IsRequired();
            b.Property(x => x.ShortDescription).HasMaxLength(255).IsRequired(false);
        });

        builder.Entity<WorkoutExercise>(b =>
        {
            b.ToTable(SyperConsts.DbTablePrefix + "WorkoutExercises",
                SyperConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.WorkoutSectionId).IsRequired();
            b.HasOne(x => x.WorkoutSection).WithMany(x => x.WorkoutExercises).HasForeignKey(x => x.WorkoutSectionId).IsRequired();
            b.Property(x => x.ExerciseId).IsRequired();
            b.HasOne(x => x.Exercise).WithMany().HasForeignKey(x => x.ExerciseId).IsRequired();
            b.HasMany(x => x.Sets).WithOne().HasForeignKey(x => x.WorkoutExerciseId).IsRequired();
            b.Property(x => x.Repeats).IsRequired(false);

        });
        
        builder.Entity<ClientCoachSubscription>(b =>
        {
            b.ToTable(SyperConsts.DbTablePrefix + "ClientCoachSubscriptions",
                SyperConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.ClientId).IsRequired();
            b.Property(x => x.TenantId).IsRequired();
            b.HasIndex(x => new { x.ClientId, x.TenantId }).IsUnique();
        });
    }
}
