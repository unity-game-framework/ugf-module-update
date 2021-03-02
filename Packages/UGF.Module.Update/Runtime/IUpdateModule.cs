using UGF.Application.Runtime;
using UGF.RuntimeTools.Runtime.Providers;
using UGF.Update.Runtime;

namespace UGF.Module.Update.Runtime
{
    public interface IUpdateModule : IApplicationModule
    {
        new IUpdateModuleDescription Description { get; }
        IUpdateProvider Provider { get; }
        IProvider<string, IUpdateSystemDescription> Systems { get; }
        IProvider<string, IUpdateGroup> Groups { get; }
    }
}
