using System;
using UGF.EditorTools.Runtime.IMGUI.Types;
using UnityEngine;

namespace UGF.Module.Update.Runtime
{
    [CreateAssetMenu(menuName = "UGF/Update/Update Group Description", order = 2000)]
    public class UpdateGroupDescriptionAsset : UpdateGroupDescriptionAssetBase
    {
        [SerializeField] private string m_name;
        [UpdateSystemTypeDropdown]
        [SerializeField] private TypeReference<object> m_systemType;

        public string Name { get { return m_name; } set { m_name = value; } }
        public TypeReference<object> SystemType { get { return m_systemType; } set { m_systemType = value; } }

        protected override IUpdateGroupDescription OnBuild()
        {
            Type systemType = m_systemType.Get();

            return new UpdateGroupDescription(m_name, systemType);
        }
    }
}
