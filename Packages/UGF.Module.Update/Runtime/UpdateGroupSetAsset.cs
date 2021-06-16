using UGF.Application.Runtime;
using UGF.Update.Runtime;
using UnityEngine;

namespace UGF.Module.Update.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Update/Update Group Set", order = 2000)]
    public class UpdateGroupSetAsset : UpdateGroupAsset<IUpdateHandler, UpdateGroupDescription>
    {
        protected override IUpdateCollection OnBuildCollection(IUpdateGroupDescription description, IApplication application)
        {
            return new UpdateSet<IUpdateHandler>();
        }
    }
}
