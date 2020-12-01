using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.IMGUI.AssetReferences;
using UnityEngine;

namespace UGF.Module.Update.Runtime
{
    [CreateAssetMenu(menuName = "UGF/Update/Update Module", order = 2000)]
    public class UpdateModuleAsset : ApplicationModuleAsset<IUpdateModule, UpdateModuleDescription>
    {
        [SerializeField] private List<AssetReference<UpdateSystemDescriptionAssetBase>> m_systems = new List<AssetReference<UpdateSystemDescriptionAssetBase>>();
        [SerializeField] private List<AssetReference<UpdateGroupAssetBase>> m_groups = new List<AssetReference<UpdateGroupAssetBase>>();

        public List<AssetReference<UpdateSystemDescriptionAssetBase>> Systems { get { return m_systems; } }
        public List<AssetReference<UpdateGroupAssetBase>> Groups { get { return m_groups; } }

        protected override IApplicationModuleDescription OnBuildDescription()
        {
            var description = new UpdateModuleDescription(typeof(IUpdateModule));

            for (int i = 0; i < m_systems.Count; i++)
            {
                AssetReference<UpdateSystemDescriptionAssetBase> reference = m_systems[i];
                IUpdateSystemDescription systemDescription = reference.Asset.Build();

                description.Systems.Add(reference.Guid, systemDescription);
            }

            for (int i = 0; i < m_groups.Count; i++)
            {
                AssetReference<UpdateGroupAssetBase> reference = m_groups[i];
                UpdateGroupAssetBase builder = reference.Asset;

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
