using System;
using UnityEngine;

namespace UGF.Module.Update.Runtime
{
    [CreateAssetMenu(menuName = "UGF/Update/Update Group Description", order = 2000)]
    public class UpdateGroupDescriptionAsset : UpdateGroupDescriptionAssetBase
    {
        [SerializeField] private string m_name;
        [UpdateSystemTypeDropdown]
        [SerializeField] private string m_systemType;

        public string Name { get { return m_name; } set { m_name = value; } }
        public string SystemType { get { return m_systemType; } set { m_systemType = value; } }

        protected override IUpdateGroupDescription OnBuild()
        {
            var systemType = Type.GetType(m_systemType, true);

            return new UpdateGroupDescription(m_name, systemType);
        }
    }
}
