using System;
using UGF.Description.Runtime;
using UGF.Update.Runtime;

namespace UGF.Module.Update.Runtime
{
    public class UpdateGroupDescribed<TItem, TDescription> : UpdateGroup<TItem>, IUpdateGroupDescribed
        where TItem : class
        where TDescription : class, IUpdateGroupDescription
    {
        public TDescription Description { get; }

        IUpdateGroupDescription IDescribed<IUpdateGroupDescription>.Description { get { return Description; } }

        public UpdateGroupDescribed(string name, IUpdateCollection<TItem> collection, TDescription description) : base(name, collection)
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
