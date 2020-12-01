using System;
using UGF.Description.Runtime;

namespace UGF.Module.Update.Runtime
{
    public class UpdateGroupDescription : DescriptionBase, IUpdateGroupDescription
    {
        public Type SystemType { get; }

        public UpdateGroupDescription(Type systemType)
        {
            SystemType = systemType ?? throw new ArgumentNullException(nameof(systemType));
        }
    }
}
