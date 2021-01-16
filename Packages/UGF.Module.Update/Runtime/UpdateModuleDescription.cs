using System.Collections.Generic;
using UGF.Application.Runtime;

namespace UGF.Module.Update.Runtime
{
    public partial class UpdateModuleDescription : ApplicationModuleDescription, IUpdateModuleDescription
    {
        public Dictionary<string, IUpdateSystemDescription> Systems { get; } = new Dictionary<string, IUpdateSystemDescription>();
        public Dictionary<string, IUpdateGroupBuilder> Groups { get; } = new Dictionary<string, IUpdateGroupBuilder>();

        IReadOnlyDictionary<string, IUpdateSystemDescription> IUpdateModuleDescription.Systems { get { return Systems; } }
        IReadOnlyDictionary<string, IUpdateGroupBuilder> IUpdateModuleDescription.Groups { get { return Groups; } }

        public UpdateModuleDescription()
        {
        }
    }
}
