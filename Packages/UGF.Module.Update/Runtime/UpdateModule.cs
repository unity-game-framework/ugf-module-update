using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UGF.Application.Runtime;
using UGF.Module.Update.Runtime.Handlers;
using UGF.Update.Runtime;
using UnityEngine.PlayerLoop;

namespace UGF.Module.Update.Runtime
{
    public class UpdateModule : ApplicationModuleBase, IUpdateModule
    {
        public IUpdateProvider Provider { get; }
        public IUpdateModuleDescription Description { get; }
        public IReadOnlyDictionary<Type, IUpdateGroup> Groups { get; }

        private readonly Dictionary<Type, IUpdateGroup> m_groups = new Dictionary<Type, IUpdateGroup>();

        public UpdateModule(IUpdateProvider provider, IUpdateModuleDescription description)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Groups = new ReadOnlyDictionary<Type, IUpdateGroup>(m_groups);

            AddGroup<Initialization, IInitializationHandler>(Description.InitializationGroupName, item => item.OnInitialization());
            AddGroup<EarlyUpdate, IEarlyUpdateHandler>(Description.EarlyUpdateGroupName, item => item.OnEarlyUpdate());
            AddGroup<FixedUpdate, IFixedUpdateHandler>(Description.FixedUpdateGroupName, item => item.OnFixedUpdate());
            AddGroup<PreUpdate, IPreUpdateHandler>(Description.PreUpdateGroupName, item => item.PreUpdate());
            AddGroup<UnityEngine.PlayerLoop.Update, IUpdateHandler>(Description.UpdateGroupName, item => item.OnUpdate());
            AddGroup<PreLateUpdate, IPreLateUpdateHandler>(Description.PreLateUpdateGroupName, item => item.OnPreLateUpdate());
            AddGroup<PostLateUpdate, IPostLateUpdateHandler>(Description.PostLateUpdateGroupName, item => item.OnPostLateUpdate());
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            foreach (KeyValuePair<Type, IUpdateGroup> pair in m_groups)
            {
                Provider.Add(pair.Key, pair.Value);
            }
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            foreach (KeyValuePair<Type, IUpdateGroup> pair in m_groups)
            {
                Provider.Remove(pair.Value.Name);
            }
        }

        public void AddGroup<TSubSystem, TItem>(string name, UpdateHandler<TItem> handler)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            var group = new UpdateGroup<TItem>(name, new UpdateSetHandler<TItem>(handler));

            AddGroup(typeof(TSubSystem), group);
        }

        public void AddGroup(Type subSystemType, IUpdateGroup group)
        {
            if (subSystemType == null) throw new ArgumentNullException(nameof(subSystemType));
            if (group == null) throw new ArgumentNullException(nameof(group));
            if (m_groups.ContainsKey(subSystemType)) throw new ArgumentException($"The group by the specified subsystem type already exists: '{subSystemType}'.", nameof(subSystemType));

            m_groups.Add(subSystemType, group);

            if (IsInitialized)
            {
                Provider.Add(subSystemType, group);
            }
        }

        public void RemoveGroup<T>()
        {
            RemoveGroup(typeof(T));
        }

        public void RemoveGroup(Type subSystemType)
        {
            if (subSystemType == null) throw new ArgumentNullException(nameof(subSystemType));

            if (IsInitialized && m_groups.TryGetValue(subSystemType, out IUpdateGroup group))
            {
                Provider.Remove(group.Name);
            }

            m_groups.Remove(subSystemType);
        }
    }
}
