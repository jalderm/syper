using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Syper.Data;

/* This is used if database provider does't define
 * ISyperDbSchemaMigrator implementation.
 */
public class NullSyperDbSchemaMigrator : ISyperDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
