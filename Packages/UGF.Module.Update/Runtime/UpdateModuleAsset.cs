using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.IMGUI.AssetReferences;
using UnityEngine;

namespace UGF.Module.Update.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Update/Update Module", order = 2000)]
    public class UpdateModuleAsset : ApplicationModuleAsset<IUpdateModule, UpdateModuleDescription>
    {
        [SerializeField] private List<AssetReference<UpdateSystemDescriptionAsset>> m_systems = new List<AssetReference<UpdateSystemDescriptionAsset>>();
        [SerializeField] private List<AssetReference<UpdateGroupAsset>> m_groups = new List<AssetReference<UpdateGroupAsset>>();

        public List<AssetReference<UpdateSystemDescriptionAsset>> Systems { get { return m_systems; } }
        public List<AssetReference<UpdateGroupAsset>> Groups { get { return m_groups; } }

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

            return description;
        }

        protected override IUpdateModule OnBuild(UpdateModuleDescription description, IApplication application)
        {
            return new UpdateModule(description, application);
        }
    }
}
