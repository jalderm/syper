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

        var exercisesPermission = myGroup.AddPermission(SyperPermissions.Exercises.Default, L("Permission:exercises"));
        exercisesPermission.AddChild(SyperPermissions.Exercises.Create, L("Permission:exercises.Create"));
        exercisesPermission.AddChild(SyperPermissions.Exercises.Edit, L("Permission:exercises.Edit"));
        exercisesPermission.AddChild(SyperPermissions.Exercises.Delete, L("Permission:exercises.Delete")); 

        var programsPermission = myGroup.AddPermission(SyperPermissions.Programs.Default, L("Permission:programs"));
        programsPermission.AddChild(SyperPermissions.Programs.Create, L("Permission:programs.Create"));
        programsPermission.AddChild(SyperPermissions.Programs.Edit, L("Permission:programs.Edit"));
        programsPermission.AddChild(SyperPermissions.Programs.Delete, L("Permission:programs.Delete")); 
        
        // var exercisesPermission = myGroup.AddPermission(SyperPermissions.Exercises.Default, L("Permission:trainingPlans"));
        // exercisesPermission.AddChild(SyperPermissions.Exercises.Create, L("Permission:trainingPlans.Create"));
        // exercisesPermission.AddChild(SyperPermissions.Exercises.Edit, L("Permission:trainingPlans.Edit"));
        // exercisesPermission.AddChild(SyperPermissions.Exercises.Delete, L("Permission:trainingPlans.Delete")); 
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SyperResource>(name);
    }
}
