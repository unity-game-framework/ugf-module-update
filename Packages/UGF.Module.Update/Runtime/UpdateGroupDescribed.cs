using System;
using UGF.Description.Runtime;
using UGF.Update.Runtime;

namespace UGF.Module.Update.Runtime
{
    public class UpdateGroupDescribed<TItem, TDescription> : UpdateGroup<TItem>, IDescribed
        where TItem : class
        where TDescription : class, IDescription
    {
        public TDescription Description { get; }

        public UpdateGroupDescribed(IUpdateCollection<TItem> collection, TDescription description) : this(collection, new UpdateListHandler<IUpdateGroup>(item => item.Update()), description)
        {
        }

        public UpdateGroupDescribed(IUpdateCollection<TItem> collection, IUpdateCollection<IUpdateGroup> subGroups, TDescription description) : base(collection, subGroups)
        {
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        public T GetDescription<T>() where T : class, IDescription
        {
            return (T)GetDescription();
        }

        public IDescription GetDescription()
        {
            return Description;
        }
    }
}
