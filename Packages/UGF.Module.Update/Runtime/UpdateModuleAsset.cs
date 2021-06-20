using System;
using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.Builder.Runtime;
using UGF.EditorTools.Runtime.IMGUI.AssetReferences;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UGF.EditorTools.Runtime.IMGUI.Types;
using UGF.Update.Runtime;
using UnityEngine;

namespace UGF.Module.Update.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Update/Update Module", order = 2000)]
    public class UpdateModuleAsset : ApplicationModuleAsset<IUpdateModule, UpdateModuleDescription>
    {
        [SerializeField] private List<SystemEntry> m_systems = new List<SystemEntry>();
        [SerializeField] private List<GroupEntry> m_groups = new List<GroupEntry>();
        [SerializeField] private List<BuilderEntry<UpdateGroupAsset>> m_subGroups = new List<BuilderEntry<UpdateGroupAsset>>();
        [SerializeField] private List<BuilderEntry<BuilderAssetBase>> m_entries = new List<BuilderEntry<BuilderAssetBase>>();

        public List<SystemEntry> Systems { get { return m_systems; } }
        public List<GroupEntry> Groups { get { return m_groups; } }
        public List<BuilderEntry<UpdateGroupAsset>> SubGroups { get { return m_subGroups; } }
        public List<BuilderEntry<BuilderAssetBase>> Entries { get { return m_entries; } }

        [Serializable]
        public struct SystemEntry
        {
            [UpdateSystemTypeDropdown]
            [SerializeField] private TypeReference<object> m_targetSystemType;
            [UpdateSystemTypeDropdown]
            [SerializeField] private TypeReference<object> m_systemType;
            [SerializeField] private UpdateSubSystemInsertion m_insertion;

            public TypeReference<object> TargetSystemType { get { return m_targetSystemType; } set { m_targetSystemType = value; } }
            public TypeReference<object> SystemType { get { return m_systemType; } set { m_systemType = value; } }
            public UpdateSubSystemInsertion Insertion { get { return m_insertion; } set { m_insertion = value; } }
        }

        [Serializable]
        public struct GroupEntry
        {
            [UpdateSystemTypeDropdown]
            [SerializeField] private TypeReference<object> m_subSystemType;
            [SerializeField] private AssetReference<UpdateGroupAsset> m_group;

            public TypeReference<object> SubSystemType { get { return m_subSystemType; } set { m_subSystemType = value; } }
            public AssetReference<UpdateGroupAsset> Group { get { return m_group; } set { m_group = value; } }
        }

        [Serializable]
        public struct BuilderEntry<TBuilder> where TBuilder : BuilderAssetBase
        {
            [AssetGuid(typeof(UpdateGroupAsset))]
            [SerializeField] private string m_group;
            [SerializeField] private AssetReference<TBuilder> m_builder;

            public string Group { get { return m_group; } set { m_group = value; } }
            public AssetReference<TBuilder> Builder { get { return m_builder; } set { m_builder = value; } }
        }

        protected override IApplicationModuleDescription OnBuildDescription()
        {
            var description = new UpdateModuleDescription
            {
                RegisterType = typeof(IUpdateModule)
            };

            for (int i = 0; i < m_systems.Count; i++)
            {
                SystemEntry entry = m_systems[i];
                Type targetSystemType = entry.TargetSystemType.Get();
                Type systemType = entry.SystemType.Get();
                UpdateSubSystemInsertion insertion = entry.Insertion;

                description.Systems.Add(new UpdateSystemDescription(targetSystemType, systemType, insertion));
            }

            for (int i = 0; i < m_groups.Count; i++)
            {
                GroupEntry entry = m_groups[i];
                Type subSystemType = entry.SubSystemType.Get();
                AssetReference<UpdateGroupAsset> reference = entry.Group;

                description.Groups.Add(reference.Guid, new UpdateGroupSystemDescription(subSystemType, reference.Asset));
            }

            for (int i = 0; i < m_subGroups.Count; i++)
            {
                BuilderEntry<UpdateGroupAsset> entry = m_subGroups[i];

                if (string.IsNullOrEmpty(entry.Group)) throw new ArgumentException("Value cannot be null or empty.", nameof(entry.Group));

                AssetReference<UpdateGroupAsset> reference = entry.Builder;

                description.SubGroups.Add(reference.Guid, new UpdateGroupItemDescription<IUpdateGroupBuilder>(entry.Group, reference.Asset));
            }

            for (int i = 0; i < m_entries.Count; i++)
            {
                BuilderEntry<BuilderAssetBase> entry = m_entries[i];

                if (string.IsNullOrEmpty(entry.Group)) throw new ArgumentException("Value cannot be null or empty.", nameof(entry.Group));

                AssetReference<BuilderAssetBase> reference = entry.Builder;

                description.Entries.Add(reference.Guid, new UpdateGroupItemDescription<IBuilder>(entry.Group, reference.Asset));
            }

            return description;
        }

        protected override IUpdateModule OnBuild(UpdateModuleDescription description, IApplication application)
        {
            return new UpdateModule(description, application);
        }
    }
}
