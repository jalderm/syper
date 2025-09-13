using Syper.Books;
using Xunit;

namespace Syper.EntityFrameworkCore.Applications.Books;

[Collection(SyperTestConsts.CollectionDefinitionName)]
public class EfCoreBookAppService_Tests : BookAppService_Tests<SyperEntityFrameworkCoreTestModule>
{

}