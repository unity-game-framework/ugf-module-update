namespace UGF.Module.Update.Runtime
{
    public interface IUpdateModuleDescription
    {
        string InitializationGroupName { get; }
        string EarlyUpdateGroupName { get; }
        string FixedUpdateGroupName { get; }
        string PreUpdateGroupName { get; }
        string UpdateGroupName { get; }
        string PreLateUpdateGroupName { get; }
        string PostLateUpdateGroupName { get; }
    }
}
