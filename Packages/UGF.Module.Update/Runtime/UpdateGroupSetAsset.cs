using UGF.Application.Runtime;
using UGF.Update.Runtime;
using UnityEngine;

namespace UGF.Module.Update.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Update/Update Group Set", order = 2000)]
    public class UpdateGroupSetAsset : UpdateGroupAsset
    {
        protected override IUpdateGroup OnBuild(IApplication arguments)
        {
            return new UpdateGroup<IUpdateHandler>(new UpdateSet<IUpdateHandler>());
        }
    }
}
