using System;
using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.Builder.Runtime;
using UGF.EditorTools.Runtime.IMGUI.AssetReferences;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEngine;

namespace UGF.Module.Update.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Update/Update Module", order = 2000)]
    public class UpdateModuleAsset : ApplicationModuleAsset<IUpdateModule, UpdateModuleDescription>
    {
        [SerializeField] private List<AssetReference<UpdateSystemDescriptionAsset>> m_systems = new List<AssetReference<UpdateSystemDescriptionAsset>>();
        [SerializeField] private List<AssetReference<UpdateGroupAsset>> m_groups = new List<AssetReference<UpdateGroupAsset>>();
        [SerializeField] private List<BuilderEntry<UpdateGroupAsset>> m_subGroups = new List<BuilderEntry<UpdateGroupAsset>>();
        [SerializeField] private List<BuilderEntry<BuilderAssetBase>> m_entries = new List<BuilderEntry<BuilderAssetBase>>();

        public List<AssetReference<UpdateSystemDescriptionAsset>> Systems { get { return m_systems; } }
        public List<AssetReference<UpdateGroupAsset>> Groups { get { return m_groups; } }
        public List<BuilderEntry<UpdateGroupAsset>> SubGroups { get { return m_subGroups; } }
        public List<BuilderEntry<BuilderAssetBase>> Entries { get { return m_entries; } }

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
