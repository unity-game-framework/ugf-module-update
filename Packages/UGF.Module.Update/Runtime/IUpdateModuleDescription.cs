using System.Collections.Generic;
using UGF.Application.Runtime;

namespace UGF.Module.Update.Runtime
{
    public interface IUpdateModuleDescription : IApplicationModuleDescription
    {
        IReadOnlyDictionary<string, IUpdateSystemDescription> Systems { get; }
        IReadOnlyDictionary<string, IUpdateGroupDescription> Groups { get; }
    }
}
