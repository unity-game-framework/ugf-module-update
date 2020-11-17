using UGF.Application.Runtime;
using UGF.Update.Runtime;

namespace UGF.Module.Update.Runtime
{
    public interface IUpdateModule : IApplicationModuleDescribed<IUpdateModuleDescription>
    {
        IUpdateProvider Provider { get; }
    }
}
