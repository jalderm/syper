using Volo.Abp.Modularity;

namespace Syper;

/* Inherit from this class for your domain layer tests. */
public abstract class SyperDomainTestBase<TStartupModule> : SyperTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
