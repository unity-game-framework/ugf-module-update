using System;
using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.Builder.Runtime;

namespace UGF.Module.Update.Runtime
{
    public class UpdateModuleDescription : ApplicationModuleDescription, IUpdateModuleDescription
    {
        public Dictionary<string, IUpdateSystemDescription> Systems { get; } = new Dictionary<string, IUpdateSystemDescription>();
        public Dictionary<string, IUpdateGroupBuilder> Groups { get; } = new Dictionary<string, IUpdateGroupBuilder>();
        public Dictionary<string, IUpdateGroupBuilder> SubGroups { get; } = new Dictionary<string, IUpdateGroupBuilder>();
        public Dictionary<string, IBuilder> Entries { get; } = new Dictionary<string, IBuilder>();

        IReadOnlyDictionary<string, IUpdateSystemDescription> IUpdateModuleDescription.Systems { get { return Systems; } }
        IReadOnlyDictionary<string, IUpdateGroupBuilder> IUpdateModuleDescription.Groups { get { return Groups; } }
    }
}
