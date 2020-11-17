using System;
using UGF.Update.Runtime;

namespace UGF.Module.Update.Runtime
{
    public interface IUpdateSystemDescription
    {
        Type TargetSystemType { get; }
        Type SystemType { get; }
        UpdateSubSystemInsertion Insertion { get; }
    }
}
