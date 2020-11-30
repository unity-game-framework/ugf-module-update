using UGF.Application.Runtime;
using UGF.Update.Runtime;

namespace UGF.Module.Update.Runtime
{
    public interface IUpdateModule : IApplicationModule
    {
        new IUpdateModuleDescription Description { get; }
        IUpdateProvider Provider { get; }

        T GetGroup<T>(string id) where T : class, IUpdateGroup;
        IUpdateGroup GetGroup(string id);
        bool TryGetGroup<T>(string id, out T group) where T : class, IUpdateGroup;
        bool TryGetGroup(string id, out IUpdateGroup group);
    }
}
