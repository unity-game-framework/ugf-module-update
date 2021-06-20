using System;
using UGF.Application.Runtime;
using UGF.Builder.Runtime;
using UGF.Description.Runtime;
using UGF.Update.Runtime;

namespace UGF.Module.Update.Runtime
{
    public abstract class UpdateGroupAsset<TDescription> : UpdateGroupAsset, IDescriptionBuilder where TDescription : class, IDescription
    {
        protected override IUpdateGroup OnBuild(IApplication arguments)
        {
            TDescription description = OnBuildDescription();

            if (description == null) throw new ArgumentNullException(nameof(description), "Description can not be null.");

            return OnBuild(description, arguments);
        }

        protected abstract TDescription OnBuildDescription();
        protected abstract IUpdateGroup OnBuild(TDescription description, IApplication application);

        T IBuilder<IDescription>.Build<T>()
        {
            return (T)(object)OnBuildDescription();
        }

        IDescription IBuilder<IDescription>.Build()
        {
            return OnBuildDescription();
        }
    }
}
