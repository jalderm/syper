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

        var workoutsPermission = myGroup.AddPermission(SyperPermissions.Workouts.Default, L("Permission:workouts"));
        workoutsPermission.AddChild(SyperPermissions.Workouts.Create, L("Permission:workouts.Create"));
        workoutsPermission.AddChild(SyperPermissions.Workouts.Edit, L("Permission:workouts.Edit"));
        workoutsPermission.AddChild(SyperPermissions.Workouts.Delete, L("Permission:workouts.Delete"));        
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SyperResource>(name);
    }
}
