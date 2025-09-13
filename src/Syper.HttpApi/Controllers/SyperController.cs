using Syper.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Syper.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class SyperController : AbpControllerBase
{
    protected SyperController()
    {
        LocalizationResource = typeof(SyperResource);
    }
}
