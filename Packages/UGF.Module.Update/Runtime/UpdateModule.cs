using System;
using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.Logs.Runtime;
using UGF.RuntimeTools.Runtime.Providers;
using UGF.Update.Runtime;

namespace UGF.Module.Update.Runtime
{
    public class UpdateModule : ApplicationModule<UpdateModuleDescription>, IUpdateModule
    {
        public IUpdateProvider Provider { get; }
        public IProvider<string, IUpdateSystemDescription> Systems { get; }
        public IProvider<string, IUpdateGroup> Groups { get; }

        IUpdateModuleDescription IUpdateModule.Description { get { return Description; } }

        public UpdateModule(UpdateModuleDescription description, IApplication application) : this(description, application, new UpdateProvider())
        {
        }

        public UpdateModule(UpdateModuleDescription description, IApplication application, IUpdateProvider provider) : this(description, application, provider, new UpdateSystemDescriptionProvider(provider), new UpdateGroupProvider(provider))
        {
        }

        public UpdateModule(UpdateModuleDescription description, IApplication application, IUpdateProvider provider, IProvider<string, IUpdateSystemDescription> systems, IProvider<string, IUpdateGroup> groups) : base(description, application)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
            Systems = systems ?? throw new ArgumentNullException(nameof(systems));
            Groups = groups ?? throw new ArgumentNullException(nameof(groups));
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Log.Debug("Update module initialize", new
            {
                systems = Description.Systems.Count,
                groups = Description.Groups.Count
            });

            foreach (KeyValuePair<string, IUpdateSystemDescription> pair in Description.Systems)
            {
                Systems.Add(pair.Key, pair.Value);
            }

            foreach (KeyValuePair<string, IUpdateGroupBuilder> pair in Description.Groups)
            {
                IUpdateGroup group = pair.Value.Build(Application);

                Groups.Add(pair.Key, group);
            }
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            Log.Debug("Update module uninitialize", new
            {
                systems = Systems.Entries.Count,
                groups = Groups.Entries.Count
            });

            Groups.Clear();
            Systems.Clear();
        }
    }
}
