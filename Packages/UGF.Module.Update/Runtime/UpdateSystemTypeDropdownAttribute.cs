using System;
using UGF.EditorTools.Runtime.IMGUI.Types;

namespace UGF.Module.Update.Runtime
{
    public class UpdateSystemTypeDropdownAttribute : TypesDropdownAttribute
    {
        public UpdateSystemTypeDropdownAttribute(Type targetType = null) : base(targetType ?? typeof(IUpdateSystemType))
        {
        }
    }
}
