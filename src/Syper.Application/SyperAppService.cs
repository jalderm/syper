using Syper.Localization;
using Volo.Abp.Application.Services;

namespace Syper;

/* Inherit your application services from this class.
 */
public abstract class SyperAppService : ApplicationService
{
    protected SyperAppService()
    {
        LocalizationResource = typeof(SyperResource);
    }
}
