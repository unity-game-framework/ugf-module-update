using UGF.Update.Runtime;

namespace UGF.Module.Update.Runtime
{
    public abstract class UpdateGroupAsset<TItem> : UpdateGroupAssetBase where TItem : class, IUpdateHandler
    {
        protected override IUpdateCollection OnBuildCollection()
        {
            return new UpdateSet<TItem>();
        }

        protected override IUpdateGroup OnBuild(IUpdateCollection collection)
        {
            return OnBuild((IUpdateCollection<TItem>)collection);
        }

        protected virtual IUpdateGroup<TItem> OnBuild(IUpdateCollection<TItem> collection)
        {
            return new UpdateGroup<TItem>(Name, collection);
        }
    }
}
