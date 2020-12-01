using System;
using UGF.EditorTools.Runtime.IMGUI.Types;
using UGF.Update.Runtime;
using UnityEngine;

namespace UGF.Module.Update.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Update/Update System Description", order = 2000)]
    public class UpdateSystemDescriptionAsset : UpdateSystemDescriptionAssetBase
    {
        [UpdateSystemTypeDropdown]
        [SerializeField] private TypeReference<object> m_target;
        [UpdateSystemTypeDropdown]
        [SerializeField] private TypeReference<object> m_type;
        [SerializeField] private UpdateSubSystemInsertion m_insertion = UpdateSubSystemInsertion.InsideBottom;

        public TypeReference<object> Target { get { return m_target; } set { m_target = value; } }
        public TypeReference<object> Type { get { return m_type; } set { m_type = value; } }
        public UpdateSubSystemInsertion Insertion { get { return m_insertion; } set { m_insertion = value; } }

        protected override IUpdateSystemDescription OnBuild()
        {
            Type targetSystemType = m_target.Get();
            Type systemType = m_type.Get();

            return new UpdateSystemDescription(targetSystemType, systemType, m_insertion);
        }
    }
}
