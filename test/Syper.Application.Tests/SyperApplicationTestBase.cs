using Volo.Abp.Modularity;

namespace Syper;

public abstract class SyperApplicationTestBase<TStartupModule> : SyperTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
