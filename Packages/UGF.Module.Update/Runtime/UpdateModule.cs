using System;
using UGF.Application.Runtime;
using UGF.Builder.Runtime;
using UGF.EditorTools.Runtime.Ids;
using UGF.Logs.Runtime;
using UGF.RuntimeTools.Runtime.Providers;
using UGF.Update.Runtime;

namespace UGF.Module.Update.Runtime
{
    public class UpdateModule : ApplicationModule<UpdateModuleDescription>, IUpdateModule
    {
        public IUpdateProvider Provider { get; }
        public IProvider<GlobalId, IUpdateGroup> Groups { get; }
        public IProvider<GlobalId, object> Entries { get; }

        IUpdateModuleDescription IUpdateModule.Description { get { return Description; } }

        public UpdateModule(UpdateModuleDescription description, IApplication application) : this(description, application, new UpdateProvider())
        {
        }

        public UpdateModule(UpdateModuleDescription description, IApplication application, IUpdateProvider provider) : this(description, application, provider, new Provider<GlobalId, IUpdateGroup>(), new Provider<GlobalId, object>())
        {
        }

        public UpdateModule(UpdateModuleDescription description, IApplication application, IUpdateProvider provider, IProvider<GlobalId, IUpdateGroup> groups, IProvider<GlobalId, object> entries) : base(description, application)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
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

            foreach (UpdateSystemDescription description in Description.Systems)
            {
                Provider.UpdateLoop.Add(description.TargetSystemType, description.SystemType, description.Insertion);
            }

            foreach ((GlobalId key, UpdateGroupSystemDescription value) in Description.Groups)
            {
                IUpdateGroup group = value.Builder.Build(Application);

                Provider.AddWithSubSystemType(group, value.SubSystemType);
                Groups.Add(key, group);
            }

            foreach ((GlobalId key, UpdateGroupItemDescription<IUpdateGroupBuilder> value) in Description.SubGroups)
            {
                IUpdateGroup group = Groups.Get(value.GroupId);
                IUpdateGroup subGroup = value.Builder.Build(Application);

                group.SubGroups.Add(subGroup);

                Groups.Add(key, subGroup);
            }

            object[] arguments = { Application };

            foreach ((GlobalId key, UpdateGroupItemDescription<IBuilder> value) in Description.Entries)
            {
                IUpdateGroup group = Groups.Get(value.GroupId);
                object entry = value.Builder.Build(arguments);

                group.Collection.Add(entry);

                Entries.Add(key, entry);
            }
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            Log.Debug("Update module uninitialize", new
            {
                systems = Description.Systems.Count,
                groups = Groups.Entries.Count,
                entries = Entries.Entries.Count
            });

            Entries.Clear();
            Groups.Clear();
            Provider.Clear();

            foreach (UpdateSystemDescription description in Description.Systems)
            {
                Provider.UpdateLoop.Remove(description.SystemType);
            }
        }
    }
}
