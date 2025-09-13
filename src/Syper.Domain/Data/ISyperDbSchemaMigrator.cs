using System.Threading.Tasks;

namespace Syper.Data;

public interface ISyperDbSchemaMigrator
{
    Task MigrateAsync();
}
