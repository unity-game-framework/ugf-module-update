using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Ids;
using UGF.RuntimeTools.Runtime.Providers;
using UGF.Update.Runtime;

namespace UGF.Module.Update.Runtime
{
    public interface IUpdateModule : IApplicationModule
    {
        new IUpdateModuleDescription Description { get; }
        IUpdateProvider Provider { get; }
        IProvider<GlobalId, IUpdateGroup> Groups { get; }
        IProvider<GlobalId, object> Entries { get; }
    }
}
