using Syper.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Syper.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(SyperEntityFrameworkCoreModule),
    typeof(SyperApplicationContractsModule)
)]
public class SyperDbMigratorModule : AbpModule
{
}
