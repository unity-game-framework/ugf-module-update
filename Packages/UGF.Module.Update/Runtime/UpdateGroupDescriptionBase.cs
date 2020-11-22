using System;
using UGF.Update.Runtime;

namespace UGF.Module.Update.Runtime
{
    public abstract class UpdateGroupDescriptionBase : IUpdateGroupDescription
    {
        public string Name { get; }
        public Type SystemType { get; }

        protected UpdateGroupDescriptionBase(string name, Type systemType)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Value cannot be null or empty.", nameof(name));

            Name = name;
            SystemType = systemType ?? throw new ArgumentNullException(nameof(systemType));
        }

        public T CreateGroup<T>() where T : class, IUpdateGroup
        {
            return (T)CreateGroup();
        }

        public IUpdateGroup CreateGroup()
        {
            IUpdateCollection collection = OnCreateUpdateCollection();

            return OnCreateGroup(Name, collection);
        }

        protected abstract IUpdateGroup OnCreateGroup(string name, IUpdateCollection collection);
        protected abstract IUpdateCollection OnCreateUpdateCollection();
    }
}
