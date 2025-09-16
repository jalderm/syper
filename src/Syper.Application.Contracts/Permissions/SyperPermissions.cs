namespace Syper.Permissions;

public static class SyperPermissions
{
    public const string GroupName = "Syper";


    public static class Clients
    {
        public const string Default = GroupName + ".Clients";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }


    public static class Workouts
    {
        public const string Default = GroupName + ".Workouts";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
}
