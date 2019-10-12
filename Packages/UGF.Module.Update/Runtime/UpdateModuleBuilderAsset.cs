using UGF.Application.Runtime;
using UGF.Module.Runtime;
using UGF.Update.Runtime;
using UnityEngine;

namespace UGF.Module.Update.Runtime
{
    [CreateAssetMenu(menuName = "UGF/Module.Update/UpdateModuleBuilder", order = 2000)]
    public class UpdateModuleBuilderAsset : ModuleBuilderAsset<IUpdateModule, UpdateModuleDescription>
    {
        protected override IApplicationModule OnBuild(IApplication application, UpdateModuleDescription description)
        {
            var updateLoop = new UpdateLoopUnity();
            var provider = new UpdateProvider(updateLoop);

            return new UpdateModule(provider, description);
        }
    }
}
