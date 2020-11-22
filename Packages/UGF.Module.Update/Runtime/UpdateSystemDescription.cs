using System;
using UGF.Update.Runtime;

namespace UGF.Module.Update.Runtime
{
    public class UpdateSystemDescription : IUpdateSystemDescription
    {
        public Type TargetSystemType { get; }
        public Type SystemType { get; }
        public UpdateSubSystemInsertion Insertion { get; }

        public UpdateSystemDescription(Type targetSystemType, Type systemType, UpdateSubSystemInsertion insertion)
        {
            TargetSystemType = targetSystemType ?? throw new ArgumentNullException(nameof(targetSystemType));
            SystemType = systemType ?? throw new ArgumentNullException(nameof(systemType));
            Insertion = insertion;
        }
    }
}
