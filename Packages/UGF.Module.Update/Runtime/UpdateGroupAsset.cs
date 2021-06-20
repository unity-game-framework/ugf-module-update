using UGF.Application.Runtime;
using UGF.Builder.Runtime;
using UGF.Update.Runtime;

namespace UGF.Module.Update.Runtime
{
    public abstract class UpdateGroupAsset : BuilderAsset<IApplication, IUpdateGroup>, IUpdateGroupBuilder
    {
    }
}
