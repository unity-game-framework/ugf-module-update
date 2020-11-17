using System;
using System.Collections.Generic;
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

        protected override void OnInitialize()
        {
            base.OnInitialize();

            foreach (KeyValuePair<string, IUpdateSystemDescription> pair in Description.Systems)
            {
                IUpdateSystemDescription systemDescription = pair.Value;

                Provider.UpdateLoop.Add(systemDescription.TargetSystemType, systemDescription.SystemType, systemDescription.Insertion);
            }
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            foreach (KeyValuePair<string, IUpdateSystemDescription> pair in Description.Systems)
            {
                IUpdateSystemDescription systemDescription = pair.Value;

                Provider.UpdateLoop.Remove(systemDescription.SystemType);
            }
        }
    }
}
