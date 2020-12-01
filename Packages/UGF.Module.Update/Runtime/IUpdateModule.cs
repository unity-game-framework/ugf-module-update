using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.Update.Runtime;

namespace UGF.Module.Update.Runtime
{
    public interface IUpdateModule : IApplicationModule
    {
        new IUpdateModuleDescription Description { get; }
        IUpdateProvider Provider { get; }
        IReadOnlyDictionary<string, IUpdateSystemDescription> Systems { get; }
        IReadOnlyDictionary<string, IUpdateGroupDescribed> Groups { get; }

        void AddSystem(string id, IUpdateSystemDescription description);
        bool RemoveSystem(string id);
        void AddGroup(string id, IUpdateGroupDescribed group);
        bool RemoveGroup(string id);
        T GetSystem<T>(string id) where T : IUpdateSystemDescription;
        IUpdateSystemDescription GetSystem(string id);
        bool TryGetSystem<T>(string id, out T description) where T : class, IUpdateSystemDescription;
        bool TryGetSystem(string id, out IUpdateSystemDescription description);
        T GetGroup<T>(string id) where T : class, IUpdateGroupDescribed;
        IUpdateGroupDescribed GetGroup(string id);
        bool TryGetGroup<T>(string id, out T group) where T : class, IUpdateGroupDescribed;
        bool TryGetGroup(string id, out IUpdateGroupDescribed group);
    }
}
