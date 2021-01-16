using System;

namespace UGF.Module.Update.Runtime
{
    public partial class UpdateModuleDescription
    {
        [Obsolete("UpdateModuleDescription constructor with 'registerType' argument has been deprecated. Use default constructor and properties initialization instead.")]
        public UpdateModuleDescription(Type registerType) : base(registerType)
        {
        }
    }
}
