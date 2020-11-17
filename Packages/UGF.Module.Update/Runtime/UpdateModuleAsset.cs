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

        public List<AssetReference<UpdateSystemDescriptionAssetBase>> Systems { get { return m_systems; } }

        protected override UpdateModuleDescription OnGetDescription(IApplication application)
        {
            var description = new UpdateModuleDescription();

            for (int i = 0; i < m_systems.Count; i++)
            {
                AssetReference<UpdateSystemDescriptionAssetBase> reference = m_systems[i];
                IUpdateSystemDescription systemDescription = reference.Asset.GetDescription();

                description.Systems.Add(reference.Guid, systemDescription);
            }

            return description;
        }

        protected override IUpdateModule OnBuild(IApplication application, UpdateModuleDescription description)
        {
            return new UpdateModule(application, description);
        }
    }
}
