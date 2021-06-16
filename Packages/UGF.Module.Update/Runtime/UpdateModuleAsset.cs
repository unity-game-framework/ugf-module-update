using System;
using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.Builder.Runtime;
using UGF.EditorTools.Runtime.IMGUI.AssetReferences;
using UnityEngine;

namespace UGF.Module.Update.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Update/Update Module", order = 2000)]
    public class UpdateModuleAsset : ApplicationModuleAsset<IUpdateModule, UpdateModuleDescription>
    {
        [SerializeField] private List<AssetReference<UpdateSystemDescriptionAsset>> m_systems = new List<AssetReference<UpdateSystemDescriptionAsset>>();
        [SerializeField] private List<AssetReference<UpdateGroupAsset>> m_groups = new List<AssetReference<UpdateGroupAsset>>();
        [SerializeField] private List<SubGroupEntry> m_subGroups = new List<SubGroupEntry>();
        [SerializeField] private List<CollectionEntry> m_entries = new List<CollectionEntry>();

        public List<AssetReference<UpdateSystemDescriptionAsset>> Systems { get { return m_systems; } }
        public List<AssetReference<UpdateGroupAsset>> Groups { get { return m_groups; } }
        public List<SubGroupEntry> SubGroups { get { return m_subGroups; } }
        public List<CollectionEntry> Entries { get { return m_entries; } }

        [Serializable]
        public struct SubGroupEntry
        {
            [SerializeField] private string m_group;
            [SerializeField] private UpdateGroupAsset m_subGroup;

            public string Group { get { return m_group; } set { m_group = value; } }
            public UpdateGroupAsset SubGroup { get { return m_subGroup; } set { m_subGroup = value; } }
        }

        [Serializable]
        public struct CollectionEntry
        {
            [SerializeField] private string m_group;
            [SerializeField] private BuilderAssetBase m_builder;

            public string Group { get { return m_group; } set { m_group = value; } }
            public BuilderAssetBase Builder { get { return m_builder; } set { m_builder = value; } }
        }

        protected override IApplicationModuleDescription OnBuildDescription()
        {
            var description = new UpdateModuleDescription
            {
                RegisterType = typeof(IUpdateModule)
            };

            for (int i = 0; i < m_systems.Count; i++)
            {
                AssetReference<UpdateSystemDescriptionAsset> reference = m_systems[i];
                IUpdateSystemDescription systemDescription = reference.Asset.Build();

                description.Systems.Add(reference.Guid, systemDescription);
            }

            for (int i = 0; i < m_groups.Count; i++)
            {
                AssetReference<UpdateGroupAsset> reference = m_groups[i];
                UpdateGroupAsset builder = reference.Asset;

                description.Groups.Add(reference.Guid, builder);
            }

            for (int i = 0; i < m_subGroups.Count; i++)
            {
                SubGroupEntry entry = m_subGroups[i];

                description.SubGroups.Add(entry.Group, entry.SubGroup);
            }

            for (int i = 0; i < m_entries.Count; i++)
            {
                CollectionEntry entry = m_entries[i];

                description.Entries.Add(entry.Group, entry.Builder);
            }

            return description;
        }

        protected override IUpdateModule OnBuild(UpdateModuleDescription description, IApplication application)
        {
            return new UpdateModule(description, application);
        }
    }
}
