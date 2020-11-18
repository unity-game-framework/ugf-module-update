using System.Collections.Generic;

namespace UGF.Module.Update.Runtime
{
    public class UpdateModuleDescription : IUpdateModuleDescription
    {
        public Dictionary<string, IUpdateSystemDescription> Systems { get; } = new Dictionary<string, IUpdateSystemDescription>();
        public Dictionary<string, IUpdateGroupDescription> Groups { get; } = new Dictionary<string, IUpdateGroupDescription>();

        IReadOnlyDictionary<string, IUpdateSystemDescription> IUpdateModuleDescription.Systems { get { return Systems; } }
        IReadOnlyDictionary<string, IUpdateGroupDescription> IUpdateModuleDescription.Groups { get { return Groups; } }
    }
}
