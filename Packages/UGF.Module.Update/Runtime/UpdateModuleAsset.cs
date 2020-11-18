using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.IMGUI.AssetReferences;
using UnityEngine;

namespace UGF.Module.Update.Runtime
{
    [CreateAssetMenu(menuName = "UGF/Update/Update Module", order = 2000)]
    public class UpdateModuleAsset : ApplicationModuleDescribedAsset<IUpdateModule, UpdateModuleDescription>
    {
        [SerializeField] private List<AssetReference<UpdateSystemDescriptionAssetBase>> m_systems = new List<AssetReference<UpdateSystemDescriptionAssetBase>>();
        [SerializeField] private List<AssetReference<UpdateGroupDescriptionAssetBase>> m_groups = new List<AssetReference<UpdateGroupDescriptionAssetBase>>();

        public List<AssetReference<UpdateSystemDescriptionAssetBase>> Systems { get { return m_systems; } }
        public List<AssetReference<UpdateGroupDescriptionAssetBase>> Groups { get { return m_groups; } }

        protected override UpdateModuleDescription OnGetDescription(IApplication application)
        {
            var description = new UpdateModuleDescription();

            for (int i = 0; i < m_systems.Count; i++)
            {
                AssetReference<UpdateSystemDescriptionAssetBase> reference = m_systems[i];
                IUpdateSystemDescription systemDescription = reference.Asset.Build();

                description.Systems.Add(reference.Guid, systemDescription);
            }

            for (int i = 0; i < m_groups.Count; i++)
            {
                AssetReference<UpdateGroupDescriptionAssetBase> reference = m_groups[i];
                IUpdateGroupDescription groupDescription = reference.Asset.Build();

                description.Groups.Add(reference.Guid, groupDescription);
            }

            return description;
        }

        protected override IUpdateModule OnBuild(IApplication application, UpdateModuleDescription description)
        {
            return new UpdateModule(application, description);
        }
    }
}
