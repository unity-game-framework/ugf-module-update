using System;
using UGF.Description.Runtime;
using UGF.Update.Runtime;

namespace UGF.Module.Update.Runtime
{
    public interface IUpdateSystemDescription : IDescription
    {
        Type TargetSystemType { get; }
        Type SystemType { get; }
        UpdateSubSystemInsertion Insertion { get; }
    }
}
