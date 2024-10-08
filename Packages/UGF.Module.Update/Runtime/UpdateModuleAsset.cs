﻿using System;
using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.Builder.Runtime;
using UGF.EditorTools.Runtime.Assets;
using UGF.EditorTools.Runtime.Ids;
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
            [SerializeField] private TypeReference m_targetSystemType;
            [UpdateSystemTypeDropdown]
            [SerializeField] private TypeReference m_systemType;
            [SerializeField] private UpdateSubSystemInsertion m_insertion;

            public TypeReference TargetSystemType { get { return m_targetSystemType; } set { m_targetSystemType = value; } }
            public TypeReference SystemType { get { return m_systemType; } set { m_systemType = value; } }
            public UpdateSubSystemInsertion Insertion { get { return m_insertion; } set { m_insertion = value; } }
        }

        [Serializable]
        public struct GroupEntry
        {
            [UpdateSystemTypeDropdown]
            [SerializeField] private TypeReference m_subSystemType;
            [SerializeField] private AssetIdReference<UpdateGroupAsset> m_group;

            public TypeReference SubSystemType { get { return m_subSystemType; } set { m_subSystemType = value; } }
            public AssetIdReference<UpdateGroupAsset> Group { get { return m_group; } set { m_group = value; } }
        }

        [Serializable]
        public struct BuilderEntry<TBuilder> where TBuilder : BuilderAssetBase
        {
            [AssetId(typeof(UpdateGroupAsset))]
            [SerializeField] private GlobalId m_group;
            [SerializeField] private AssetIdReference<TBuilder> m_builder;

            public GlobalId Group { get { return m_group; } set { m_group = value; } }
            public AssetIdReference<TBuilder> Builder { get { return m_builder; } set { m_builder = value; } }
        }

        protected override UpdateModuleDescription OnBuildDescription()
        {
            var systems = new List<UpdateSystemDescription>();
            var groups = new Dictionary<GlobalId, UpdateGroupSystemDescription>();
            var subGroups = new Dictionary<GlobalId, UpdateGroupItemDescription<IUpdateGroupBuilder>>();
            var entries = new Dictionary<GlobalId, UpdateGroupItemDescription<IBuilder>>();

            for (int i = 0; i < m_systems.Count; i++)
            {
                SystemEntry entry = m_systems[i];
                Type targetSystemType = entry.TargetSystemType.Get();
                Type systemType = entry.SystemType.Get();
                UpdateSubSystemInsertion insertion = entry.Insertion;

                systems.Add(new UpdateSystemDescription(targetSystemType, systemType, insertion));
            }

            for (int i = 0; i < m_groups.Count; i++)
            {
                GroupEntry entry = m_groups[i];
                Type subSystemType = entry.SubSystemType.Get();
                AssetIdReference<UpdateGroupAsset> reference = entry.Group;

                groups.Add(reference.Guid, new UpdateGroupSystemDescription(subSystemType, reference.Asset));
            }

            for (int i = 0; i < m_subGroups.Count; i++)
            {
                BuilderEntry<UpdateGroupAsset> entry = m_subGroups[i];

                if (!entry.Group.IsValid()) throw new ArgumentException("Value should be valid.", nameof(entry.Group));

                AssetIdReference<UpdateGroupAsset> reference = entry.Builder;

                subGroups.Add(reference.Guid, new UpdateGroupItemDescription<IUpdateGroupBuilder>(entry.Group, reference.Asset));
            }

            for (int i = 0; i < m_entries.Count; i++)
            {
                BuilderEntry<BuilderAssetBase> entry = m_entries[i];

                if (!entry.Group.IsValid()) throw new ArgumentException("Value should be valid.", nameof(entry.Group));

                AssetIdReference<BuilderAssetBase> reference = entry.Builder;

                entries.Add(reference.Guid, new UpdateGroupItemDescription<IBuilder>(entry.Group, reference.Asset));
            }

            return new UpdateModuleDescription(systems, groups, subGroups, entries);
        }

        protected override IUpdateModule OnBuild(UpdateModuleDescription description, IApplication application)
        {
            return new UpdateModule(description, application);
        }
    }
}
