using System;
using UGF.Application.Runtime;
using UGF.Builder.Runtime;
using UGF.Description.Runtime;
using UGF.Update.Runtime;

namespace UGF.Module.Update.Runtime
{
    public abstract class UpdateGroupAsset : BuilderAsset<IApplication, IUpdateGroup>, IUpdateGroupBuilder, IDescriptionBuilder
    {
        protected override IUpdateGroup OnBuild(IApplication arguments)
        {
            IUpdateGroupDescription description = OnBuildDescription();

            if (description == null) throw new ArgumentNullException(nameof(description), "Description can not be null.");

            return OnBuild(description, arguments);
        }

        protected virtual IUpdateGroup OnBuild(IUpdateGroupDescription description, IApplication application)
        {
            IUpdateCollection collection = OnBuildCollection(description, application);

            return OnBuild(collection, description, application);
        }

        protected abstract IUpdateGroupDescription OnBuildDescription();
        protected abstract IUpdateCollection OnBuildCollection(IUpdateGroupDescription description, IApplication application);
        protected abstract IUpdateGroup OnBuild(IUpdateCollection collection, IUpdateGroupDescription description, IApplication application);

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
