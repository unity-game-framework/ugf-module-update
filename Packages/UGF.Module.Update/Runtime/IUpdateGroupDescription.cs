using System;
using UGF.Update.Runtime;

namespace UGF.Module.Update.Runtime
{
    public interface IUpdateGroupDescription
    {
        string Name { get; }
        Type SystemType { get; }

        T CreateGroup<T>() where T : class, IUpdateGroup;
        IUpdateGroup CreateGroup();
    }
}
