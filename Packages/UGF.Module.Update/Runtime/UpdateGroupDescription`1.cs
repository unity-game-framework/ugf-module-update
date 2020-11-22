using System;
using UGF.Update.Runtime;

namespace UGF.Module.Update.Runtime
{
    public class UpdateGroupDescription<TItem> : UpdateGroupDescriptionBase where TItem : class, IUpdateHandler
    {
        public UpdateGroupDescription(string name, Type systemType) : base(name, systemType)
        {
        }

        protected override IUpdateGroup OnCreateGroup(string name, IUpdateCollection collection)
        {
            return OnCreateGroup(name, (IUpdateCollection<TItem>)collection);
        }

        protected virtual IUpdateGroup<TItem> OnCreateGroup(string name, IUpdateCollection<TItem> collection)
        {
            return new UpdateGroup<TItem>(name, collection);
        }

        protected override IUpdateCollection OnCreateUpdateCollection()
        {
            return new UpdateSet<TItem>();
        }
    }
}
