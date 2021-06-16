using System;
using UGF.Description.Runtime;
using UGF.Update.Runtime;

namespace UGF.Module.Update.Runtime
{
    public class UpdateGroupDescribed<TItem, TDescription> : UpdateGroup<TItem>, IDescribed<IUpdateGroupDescription>
        where TItem : class
        where TDescription : class, IUpdateGroupDescription
    {
        public TDescription Description { get; }

        IUpdateGroupDescription IDescribed<IUpdateGroupDescription>.Description { get { return Description; } }

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
