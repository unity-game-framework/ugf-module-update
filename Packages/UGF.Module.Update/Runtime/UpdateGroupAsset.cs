using UGF.Update.Runtime;
using UnityEngine;

namespace UGF.Module.Update.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Update/Update Group", order = 2000)]
    public class UpdateGroupAsset : UpdateGroupAsset<IUpdateHandler, UpdateGroupDescription>
    {
        protected override IUpdateCollection OnBuildCollection()
        {
            return new UpdateSet<IUpdateHandler>();
        }
    }
}
