using UGF.Application.Runtime;
using UGF.Module.Runtime;
using UnityEngine;

namespace UGF.Module.Update.Runtime
{
    [CreateAssetMenu(menuName = "UGF/Module.Update/UpdateModuleBuilder", order = 2000)]
    public class UpdateModuleBuilderAsset : ModuleBuilderAsset<IUpdateModule>
    {
        protected override IApplicationModule OnBuild(IApplication application, IModuleBuildArguments<object> arguments)
        {
            return new UpdateModule();
        }
    }
}
