using System;
using UGF.EditorTools.Runtime.IMGUI.Types;

namespace UGF.Module.Update.Runtime
{
    public class UpdateSystemTypeDropdownAttribute : TypesDropdownAttributeBase
    {
        public override Type TargetType { get; } = typeof(IUpdateSystemType);
    }
}
