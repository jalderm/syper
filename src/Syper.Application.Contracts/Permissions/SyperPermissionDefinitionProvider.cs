using Syper.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Syper.Permissions;

public class SyperPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(SyperPermissions.GroupName);

        var clientsPermission = myGroup.AddPermission(SyperPermissions.Clients.Default, L("Permission:clients"));
        clientsPermission.AddChild(SyperPermissions.Clients.Create, L("Permission:clients.Create"));
        clientsPermission.AddChild(SyperPermissions.Clients.Edit, L("Permission:clients.Edit"));
        clientsPermission.AddChild(SyperPermissions.Clients.Delete, L("Permission:clients.Delete"));
        //Define your own permissions here. Example:
        //myGroup.AddPermission(SyperPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SyperResource>(name);
    }
}
