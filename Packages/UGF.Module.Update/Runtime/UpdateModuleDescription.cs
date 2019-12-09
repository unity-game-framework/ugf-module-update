namespace UGF.Module.Update.Runtime
{
    public class UpdateModuleDescription : IUpdateModuleDescription
    {
        public string InitializationGroupName { get; set; } = "UpdateModule.Initialization";
        public string EarlyUpdateGroupName { get; set; } = "UpdateModule.EarlyUpdate";
        public string FixedUpdateGroupName { get; set; } = "UpdateModule.FixedUpdate";
        public string PreUpdateGroupName { get; set; } = "UpdateModule.PreUpdate";
        public string UpdateGroupName { get; set; } = "UpdateModule.Update";
        public string PreLateUpdateGroupName { get; set; } = "UpdateModule.PreLateUpdate";
        public string PostLateUpdateGroupName { get; set; } = "UpdateModule.PostLateUpdate";
    }
}
