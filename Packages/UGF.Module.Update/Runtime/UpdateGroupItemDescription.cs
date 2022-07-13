using System;
using UGF.Builder.Runtime;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Update.Runtime
{
    public readonly struct UpdateGroupItemDescription<TBuilder> where TBuilder : class, IBuilder
    {
        public GlobalId GroupId { get; }
        public TBuilder Builder { get; }

        public UpdateGroupItemDescription(GlobalId groupId, TBuilder builder)
        {
            if (!groupId.IsValid()) throw new ArgumentException("Value should be valid.", nameof(groupId));

            GroupId = groupId;
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }
    }
}
