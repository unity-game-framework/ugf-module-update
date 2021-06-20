using System;
using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.Builder.Runtime;
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
        public IProvider<string, object> Entries { get; }

        IUpdateModuleDescription IUpdateModule.Description { get { return Description; } }

        public UpdateModule(UpdateModuleDescription description, IApplication application) : this(description, application, new UpdateProvider())
        {
        }

        public UpdateModule(UpdateModuleDescription description, IApplication application, IUpdateProvider provider) : this(description, application, provider, new UpdateSystemDescriptionProvider(provider.UpdateLoop), new Provider<string, IUpdateGroup>(), new Provider<string, object>())
        {
        }

        public UpdateModule(UpdateModuleDescription description, IApplication application, IUpdateProvider provider, IProvider<string, IUpdateSystemDescription> systems, IProvider<string, IUpdateGroup> groups, IProvider<string, object> entries) : base(description, application)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
            Systems = systems ?? throw new ArgumentNullException(nameof(systems));
            Groups = groups ?? throw new ArgumentNullException(nameof(groups));
            Entries = entries ?? throw new ArgumentNullException(nameof(entries));
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Log.Debug("Update module initialize", new
            {
                systems = Description.Systems.Count,
                groups = Description.Groups.Count,
                subGroups = Description.SubGroups.Count,
                entries = Description.Entries.Count
            });

            foreach (KeyValuePair<string, IUpdateSystemDescription> pair in Description.Systems)
            {
                Systems.Add(pair.Key, pair.Value);
            }

            foreach (KeyValuePair<string, UpdateGroupSystemDescription> pair in Description.Groups)
            {
                IUpdateGroup group = pair.Value.Builder.Build(Application);

                Provider.AddWithSubSystemType(group, pair.Value.SubSystemType);
                Groups.Add(pair.Key, group);
            }

            foreach (KeyValuePair<string, UpdateGroupItemDescription<IUpdateGroupBuilder>> pair in Description.SubGroups)
            {
                IUpdateGroup group = Groups.Get(pair.Value.GroupId);
                IUpdateGroup subGroup = pair.Value.Builder.Build(Application);

                group.SubGroups.Add(subGroup);

                Groups.Add(pair.Key, subGroup);
            }

            object[] arguments = { Application };

            foreach (KeyValuePair<string, UpdateGroupItemDescription<IBuilder>> pair in Description.Entries)
            {
                IUpdateGroup group = Groups.Get(pair.Key);
                object entry = pair.Value.Builder.Build(arguments);

                group.Collection.Add(entry);

                Entries.Add(pair.Key, entry);
            }
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            Log.Debug("Update module uninitialize", new
            {
                systems = Systems.Entries.Count,
                groups = Groups.Entries.Count,
                entries = Entries.Entries.Count
            });

            Provider.Clear();
            Groups.Clear();
            Systems.Clear();
            Entries.Clear();
        }
    }
}
