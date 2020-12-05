using UGF.Description.Runtime;
using UGF.Update.Runtime;

namespace UGF.Module.Update.Runtime
{
    public interface IUpdateGroupDescribed : IUpdateGroup, IDescribed<IUpdateGroupDescription>
    {
    }
}
