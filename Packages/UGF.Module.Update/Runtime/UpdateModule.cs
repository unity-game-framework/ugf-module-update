using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UGF.Application.Runtime;
using UGF.Logs.Runtime;
using UGF.Update.Runtime;

namespace UGF.Module.Update.Runtime
{
    public class UpdateModule : ApplicationModule<UpdateModuleDescription>, IUpdateModule
    {
        public IUpdateProvider Provider { get; }
        public IReadOnlyDictionary<string, IUpdateSystemDescription> Systems { get; }
        public IReadOnlyDictionary<string, IUpdateGroupDescribed> Groups { get; }

        IUpdateModuleDescription IUpdateModule.Description { get { return Description; } }

        private readonly Dictionary<string, IUpdateSystemDescription> m_systems = new Dictionary<string, IUpdateSystemDescription>();
        private readonly Dictionary<string, IUpdateGroupDescribed> m_groups = new Dictionary<string, IUpdateGroupDescribed>();

        public UpdateModule(UpdateModuleDescription description, IApplication application) : this(description, application, new UpdateProvider())
        {
        }

        public UpdateModule(UpdateModuleDescription description, IApplication application, IUpdateProvider provider) : base(description, application)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
            Systems = new ReadOnlyDictionary<string, IUpdateSystemDescription>(m_systems);
            Groups = new ReadOnlyDictionary<string, IUpdateGroupDescribed>(m_groups);
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
                AddSystem(pair.Key, pair.Value);
            }

            foreach (KeyValuePair<string, IUpdateGroupBuilder> pair in Description.Groups)
            {
                IUpdateGroupDescribed group = pair.Value.Build();

                AddGroup(pair.Key, group);
            }
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            Log.Debug("Update module uninitialize", new
            {
                systems = m_systems.Count,
                groups = m_groups.Count
            });

            while (m_groups.Count > 0)
            {
                string id = m_groups.First().Key;

                RemoveGroup(id);
            }

            while (m_systems.Count > 0)
            {
                string id = m_systems.First().Key;

                RemoveSystem(id);
            }
        }

        public void AddSystem(string id, IUpdateSystemDescription description)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (description == null) throw new ArgumentNullException(nameof(description));

            Provider.UpdateLoop.Add(description.TargetSystemType, description.SystemType, description.Insertion);

            m_systems.Add(id, description);

            Log.Debug("Add update system", new
            {
                id,
                description.TargetSystemType,
                description.SystemType,
                description.Insertion
            });
        }

        public bool RemoveSystem(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(id));

            if (TryGetSystem(id, out IUpdateSystemDescription description))
            {
                Provider.UpdateLoop.Remove(description.SystemType);

                Log.Debug("Remove update system", new
                {
                    id,
                    description.TargetSystemType,
                    description.SystemType,
                    description.Insertion
                });

                return m_systems.Remove(id);
            }

            return false;
        }

        public void AddGroup(string id, IUpdateGroupDescribed group)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (group == null) throw new ArgumentNullException(nameof(group));

            Provider.Add(group.Description.SystemType, group);

            m_groups.Add(id, group);

            Log.Debug("Add update group", new
            {
                id,
                group.Name,
                group.Description.SystemType
            });
        }

        public bool RemoveGroup(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            if (TryGetGroup(id, out IUpdateGroupDescribed group))
            {
                Provider.Remove(group);

                Log.Debug("Remove update group", new
                {
                    id,
                    group.Name,
                    group.Description.SystemType
                });

                return m_groups.Remove(id);
            }

            return false;
        }

        public T GetSystem<T>(string id) where T : IUpdateSystemDescription
        {
            return (T)GetSystem(id);
        }

        public IUpdateSystemDescription GetSystem(string id)
        {
            return TryGetSystem(id, out IUpdateSystemDescription description) ? description : throw new ArgumentException($"Update system not found by the specified id: '{id}'.");
        }

        public bool TryGetSystem<T>(string id, out T description) where T : class, IUpdateSystemDescription
        {
            if (TryGetSystem(id, out IUpdateSystemDescription value))
            {
                description = (T)value;
                return true;
            }

            description = default;
            return false;
        }

        public bool TryGetSystem(string id, out IUpdateSystemDescription description)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(id));

            return m_systems.TryGetValue(id, out description);
        }

        public T GetGroup<T>(string id) where T : class, IUpdateGroupDescribed
        {
            return (T)GetGroup(id);
        }

        public IUpdateGroupDescribed GetGroup(string id)
        {
            return TryGetGroup(id, out IUpdateGroupDescribed group) ? group : throw new ArgumentException($"Update group not found by the specified id: '{id}'.");
        }

        public bool TryGetGroup<T>(string id, out T group) where T : class, IUpdateGroupDescribed
        {
            if (TryGetGroup(id, out IUpdateGroupDescribed value))
            {
                group = (T)value;
                return true;
            }

            group = default;
            return false;
        }

        public bool TryGetGroup(string id, out IUpdateGroupDescribed group)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(id));

            return m_groups.TryGetValue(id, out group);
        }
    }
}
