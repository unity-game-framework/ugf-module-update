using UGF.Application.Runtime;
using UGF.Module.Update.Runtime.Handlers;
using UGF.Update.Runtime;

namespace UGF.Module.Update.Runtime
{
    public interface IUpdateModule : IApplicationModule
    {
        IUpdateCollection<IPreUpdateHandler> PreUpdate { get; }
        IUpdateCollection<IUpdateHandler> Update { get; }
        IUpdateCollection<IFixedUpdateHandler> FixedUpdate { get; }
        IUpdateCollection<IPostLateUpdateHandler> PostLateUpdate { get; }
    }
}
