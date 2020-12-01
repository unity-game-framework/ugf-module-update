using System;
using UGF.Description.Runtime;

namespace UGF.Module.Update.Runtime
{
    public interface IUpdateGroupDescription : IDescription
    {
        Type SystemType { get; }
    }
}
