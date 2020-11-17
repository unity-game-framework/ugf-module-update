using UGF.Application.Runtime;
using UnityEngine;

namespace UGF.Module.Update.Runtime
{
    [CreateAssetMenu(menuName = "UGF/Update/Update Module", order = 2000)]
    public class UpdateModuleAsset : ApplicationModuleDescribedAsset<IUpdateModule, UpdateModuleDescription>
    {
        protected override UpdateModuleDescription OnGetDescription(IApplication application)
        {
            var description = new UpdateModuleDescription();

            return description;
        }

        protected override IUpdateModule OnBuild(IApplication application, UpdateModuleDescription description)
        {
            return new UpdateModule(application, description);
        }
    }
}
