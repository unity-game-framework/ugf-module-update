using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.Builder.Runtime;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Update.Runtime
{
    public interface IUpdateModuleDescription : IApplicationModuleDescription
    {
        IReadOnlyList<UpdateSystemDescription> Systems { get; }
        IReadOnlyDictionary<GlobalId, UpdateGroupSystemDescription> Groups { get; }
        IReadOnlyDictionary<GlobalId, UpdateGroupItemDescription<IUpdateGroupBuilder>> SubGroups { get; }
        IReadOnlyDictionary<GlobalId, UpdateGroupItemDescription<IBuilder>> Entries { get; }
    }
}
