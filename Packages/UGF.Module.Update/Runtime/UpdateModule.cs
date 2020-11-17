using System;
using UGF.Application.Runtime;
using UGF.Update.Runtime;

namespace UGF.Module.Update.Runtime
{
    public class UpdateModule : ApplicationModuleDescribed<UpdateModuleDescription>, IUpdateModule
    {
        public IUpdateProvider Provider { get; }

        IUpdateModuleDescription IApplicationModuleDescribed<IUpdateModuleDescription>.Description { get { return Description; } }

        public UpdateModule(IApplication application, UpdateModuleDescription description) : this(application, description, new UpdateProvider())
        {
        }

        public UpdateModule(IApplication application, UpdateModuleDescription description, IUpdateProvider provider) : base(application, description)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }
    }
}
