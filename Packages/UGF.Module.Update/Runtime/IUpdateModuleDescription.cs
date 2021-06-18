using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.Builder.Runtime;

namespace UGF.Module.Update.Runtime
{
    public interface IUpdateModuleDescription : IApplicationModuleDescription
    {
        IReadOnlyDictionary<string, IUpdateSystemDescription> Systems { get; }
        IReadOnlyDictionary<string, IUpdateGroupBuilder> Groups { get; }
        IReadOnlyDictionary<string, UpdateGroupItemDescription<IUpdateGroupBuilder>> SubGroups { get; }
        IReadOnlyDictionary<string, UpdateGroupItemDescription<IBuilder>> Entries { get; }
    }
}
