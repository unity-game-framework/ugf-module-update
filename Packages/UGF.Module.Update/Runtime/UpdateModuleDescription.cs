using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.Builder.Runtime;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Update.Runtime
{
    public class UpdateModuleDescription : ApplicationModuleDescription, IUpdateModuleDescription
    {
        public List<UpdateSystemDescription> Systems { get; } = new List<UpdateSystemDescription>();
        public Dictionary<GlobalId, UpdateGroupSystemDescription> Groups { get; } = new Dictionary<GlobalId, UpdateGroupSystemDescription>();
        public Dictionary<GlobalId, UpdateGroupItemDescription<IUpdateGroupBuilder>> SubGroups { get; } = new Dictionary<GlobalId, UpdateGroupItemDescription<IUpdateGroupBuilder>>();
        public Dictionary<GlobalId, UpdateGroupItemDescription<IBuilder>> Entries { get; } = new Dictionary<GlobalId, UpdateGroupItemDescription<IBuilder>>();

        IReadOnlyList<UpdateSystemDescription> IUpdateModuleDescription.Systems { get { return Systems; } }
        IReadOnlyDictionary<GlobalId, UpdateGroupSystemDescription> IUpdateModuleDescription.Groups { get { return Groups; } }
        IReadOnlyDictionary<GlobalId, UpdateGroupItemDescription<IUpdateGroupBuilder>> IUpdateModuleDescription.SubGroups { get { return SubGroups; } }
        IReadOnlyDictionary<GlobalId, UpdateGroupItemDescription<IBuilder>> IUpdateModuleDescription.Entries { get { return Entries; } }
    }
}
