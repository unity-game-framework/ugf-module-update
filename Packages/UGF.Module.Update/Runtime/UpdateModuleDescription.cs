using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.Builder.Runtime;

namespace UGF.Module.Update.Runtime
{
    public class UpdateModuleDescription : ApplicationModuleDescription, IUpdateModuleDescription
    {
        public List<UpdateSystemDescription> Systems { get; } = new List<UpdateSystemDescription>();
        public Dictionary<string, UpdateGroupSystemDescription> Groups { get; } = new Dictionary<string, UpdateGroupSystemDescription>();
        public Dictionary<string, UpdateGroupItemDescription<IUpdateGroupBuilder>> SubGroups { get; } = new Dictionary<string, UpdateGroupItemDescription<IUpdateGroupBuilder>>();
        public Dictionary<string, UpdateGroupItemDescription<IBuilder>> Entries { get; } = new Dictionary<string, UpdateGroupItemDescription<IBuilder>>();

        IReadOnlyList<UpdateSystemDescription> IUpdateModuleDescription.Systems { get { return Systems; } }
        IReadOnlyDictionary<string, UpdateGroupSystemDescription> IUpdateModuleDescription.Groups { get { return Groups; } }
        IReadOnlyDictionary<string, UpdateGroupItemDescription<IUpdateGroupBuilder>> IUpdateModuleDescription.SubGroups { get { return SubGroups; } }
        IReadOnlyDictionary<string, UpdateGroupItemDescription<IBuilder>> IUpdateModuleDescription.Entries { get { return Entries; } }
    }
}
