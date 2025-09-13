using Xunit;

namespace Syper.EntityFrameworkCore;

[CollectionDefinition(SyperTestConsts.CollectionDefinitionName)]
public class SyperEntityFrameworkCoreCollection : ICollectionFixture<SyperEntityFrameworkCoreFixture>
{

}
