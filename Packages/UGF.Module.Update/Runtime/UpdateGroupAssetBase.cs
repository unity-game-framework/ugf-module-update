using UGF.Description.Runtime;
using UGF.Update.Runtime;

namespace UGF.Module.Update.Runtime
{
    public abstract class UpdateGroupAssetBase : DescribedWithDescriptionBuilderAsset<IUpdateGroupDescribed, IUpdateGroupDescription>, IUpdateGroupBuilder
    {
        protected override IUpdateGroupDescribed OnBuild(IUpdateGroupDescription description)
        {
            IUpdateCollection collection = OnBuildCollection();

            return OnBuild(collection, description);
        }

        protected abstract IUpdateCollection OnBuildCollection();
        protected abstract IUpdateGroupDescribed OnBuild(IUpdateCollection collection, IUpdateGroupDescription description);
    }
}
