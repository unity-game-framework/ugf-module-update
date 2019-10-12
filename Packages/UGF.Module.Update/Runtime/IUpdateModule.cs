using System;
using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.Update.Runtime;

namespace UGF.Module.Update.Runtime
{
    public interface IUpdateModule : IApplicationModule
    {
        IUpdateProvider Provider { get; }
        IUpdateModuleDescription Description { get; }
        IReadOnlyDictionary<Type, IUpdateGroup> Groups { get; }

        void AddGroup<TSubSystem, TItem>(string name, UpdateHandler<TItem> handler);
        void AddGroup(Type subSystemType, IUpdateGroup group);
        void RemoveGroup<T>();
        void RemoveGroup(Type subSystemType);
    }
}
