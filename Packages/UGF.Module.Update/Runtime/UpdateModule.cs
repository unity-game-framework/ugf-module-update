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

            foreach (KeyValuePair<string, IUpdateGroupDescription> pair in Description.Groups)
            {
                IUpdateGroupDescription groupDescription = pair.Value;
                IUpdateGroup group = groupDescription.CreateGroup();

                Provider.Add(groupDescription.SystemType, group);
            }
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            foreach (KeyValuePair<string, IUpdateGroupDescription> pair in Description.Groups)
            {
                IUpdateGroupDescription groupDescription = pair.Value;

                Provider.Remove(groupDescription.Name);
            }

            foreach (KeyValuePair<string, IUpdateSystemDescription> pair in Description.Systems)
            {
                IUpdateSystemDescription systemDescription = pair.Value;

                Provider.UpdateLoop.Remove(systemDescription.SystemType);
            }
        }

        public T GetGroup<T>(string id) where T : class, IUpdateGroup
        {
            return (T)GetGroup(id);
        }

        public IUpdateGroup GetGroup(string id)
        {
            return TryGetGroup(id, out IUpdateGroup group) ? group : throw new ArgumentException($"Group not found by the specified id: '{id}'.");
        }

        public bool TryGetGroup<T>(string id, out T group) where T : class, IUpdateGroup
        {
            if (TryGetGroup(id, out IUpdateGroup value))
            {
                group = (T)value;
                return true;
            }

            group = default;
            return false;
        }

        public bool TryGetGroup(string id, out IUpdateGroup group)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            group = default;
            return Description.Groups.TryGetValue(id, out IUpdateGroupDescription description) && Provider.TryGetGroup(description.Name, out group);
        }
    }
}
