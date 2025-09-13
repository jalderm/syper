using Volo.Abp.Modularity;

namespace Syper;

[DependsOn(
    typeof(SyperApplicationModule),
    typeof(SyperDomainTestModule)
)]
public class SyperApplicationTestModule : AbpModule
{

}
