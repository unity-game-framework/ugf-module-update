using System;
using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.Builder.Runtime;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Update.Runtime
{
    public class UpdateModuleDescription : ApplicationModuleDescription, IUpdateModuleDescription
    {
        public IReadOnlyList<UpdateSystemDescription> Systems { get; }
        public IReadOnlyDictionary<GlobalId, UpdateGroupSystemDescription> Groups { get; }
        public IReadOnlyDictionary<GlobalId, UpdateGroupItemDescription<IUpdateGroupBuilder>> SubGroups { get; }
        public IReadOnlyDictionary<GlobalId, UpdateGroupItemDescription<IBuilder>> Entries { get; }

        public UpdateModuleDescription(
            IReadOnlyList<UpdateSystemDescription> systems,
            IReadOnlyDictionary<GlobalId, UpdateGroupSystemDescription> groups,
            IReadOnlyDictionary<GlobalId, UpdateGroupItemDescription<IUpdateGroupBuilder>> subGroups,
            IReadOnlyDictionary<GlobalId, UpdateGroupItemDescription<IBuilder>> entries)
        {
            Systems = systems ?? throw new ArgumentNullException(nameof(systems));
            Groups = groups ?? throw new ArgumentNullException(nameof(groups));
            SubGroups = subGroups ?? throw new ArgumentNullException(nameof(subGroups));
            Entries = entries ?? throw new ArgumentNullException(nameof(entries));
        }
    }
}
