using System;
using UGF.Application.Runtime;
using UGF.Builder.Runtime;
using UGF.Description.Runtime;
using UGF.Update.Runtime;

namespace UGF.Module.Update.Runtime
{
    public abstract class UpdateGroupAssetBase : BuilderAsset<IApplication, IUpdateGroup>, IUpdateGroupBuilder, IDescriptionBuilder
    {
        protected override IUpdateGroup OnBuild(IApplication arguments)
        {
            IUpdateGroupDescription description = OnBuildDescription();

            if (description == null) throw new ArgumentNullException(nameof(description), "Description can not be null.");

            return OnBuild(description);
        }

        protected virtual IUpdateGroup OnBuild(IUpdateGroupDescription description)
        {
            IUpdateCollection collection = OnBuildCollection();

            return OnBuild(collection, description);
        }

        protected abstract IUpdateGroupDescription OnBuildDescription();
        protected abstract IUpdateCollection OnBuildCollection();
        protected abstract IUpdateGroup OnBuild(IUpdateCollection collection, IUpdateGroupDescription description);

        T IBuilder<IDescription>.Build<T>()
        {
            return (T)OnBuildDescription();
        }

        IDescription IBuilder<IDescription>.Build()
        {
            return OnBuildDescription();
        }
    }
}
