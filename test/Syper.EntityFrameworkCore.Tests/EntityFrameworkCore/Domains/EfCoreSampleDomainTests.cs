using Syper.Samples;
using Xunit;

namespace Syper.EntityFrameworkCore.Domains;

[Collection(SyperTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<SyperEntityFrameworkCoreTestModule>
{

}
