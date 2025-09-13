using Volo.Abp.Settings;

namespace Syper.Settings;

public class SyperSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(SyperSettings.MySetting1));
    }
}
