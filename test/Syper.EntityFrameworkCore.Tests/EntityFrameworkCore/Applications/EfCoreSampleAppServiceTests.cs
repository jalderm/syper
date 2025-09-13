using Syper.Samples;
using Xunit;

namespace Syper.EntityFrameworkCore.Applications;

[Collection(SyperTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<SyperEntityFrameworkCoreTestModule>
{

}
