using System;

namespace UGF.Module.Update.Runtime
{
    public readonly struct UpdateGroupSystemDescription
    {
        public Type SubSystemType { get; }
        public IUpdateGroupBuilder Builder { get; }

        public UpdateGroupSystemDescription(Type subSystemType, IUpdateGroupBuilder builder)
        {
            SubSystemType = subSystemType ?? throw new ArgumentNullException(nameof(subSystemType));
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }
    }
}
