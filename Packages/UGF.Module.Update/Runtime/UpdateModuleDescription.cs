using System.Collections.Generic;

namespace UGF.Module.Update.Runtime
{
    public class UpdateModuleDescription : IUpdateModuleDescription
    {
        public Dictionary<string, IUpdateSystemDescription> Systems { get; } = new Dictionary<string, IUpdateSystemDescription>();

        IReadOnlyDictionary<string, IUpdateSystemDescription> IUpdateModuleDescription.Systems { get { return Systems; } }
    }
}
