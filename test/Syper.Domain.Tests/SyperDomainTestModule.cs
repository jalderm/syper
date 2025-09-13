using Volo.Abp.Modularity;

namespace Syper;

[DependsOn(
    typeof(SyperDomainModule),
    typeof(SyperTestBaseModule)
)]
public class SyperDomainTestModule : AbpModule
{

}
