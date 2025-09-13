using Microsoft.Extensions.Localization;
using Syper.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Syper;

[Dependency(ReplaceServices = true)]
public class SyperBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<SyperResource> _localizer;

    public SyperBrandingProvider(IStringLocalizer<SyperResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
