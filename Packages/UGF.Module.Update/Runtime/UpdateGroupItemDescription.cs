using System;
using UGF.Builder.Runtime;

namespace UGF.Module.Update.Runtime
{
    public readonly struct UpdateGroupItemDescription<TBuilder> where TBuilder : class, IBuilder
    {
        public string GroupId { get; }
        public TBuilder Builder { get; }

        public UpdateGroupItemDescription(string groupId, TBuilder builder)
        {
            if (string.IsNullOrEmpty(groupId)) throw new ArgumentException("Value cannot be null or empty.", nameof(groupId));

            GroupId = groupId;
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }
    }
}
