using UGF.Update.Runtime;
using UnityEngine;

namespace UGF.Module.Update.Runtime
{
    [CreateAssetMenu(menuName = "UGF/Update/Update System Description", order = 2000)]
    public class UpdateSystemDescriptionAsset : UpdateSystemDescriptionAssetBase
    {
        [UpdateSystemTypeDropdown]
        [SerializeField] private string m_target;
        [UpdateSystemTypeDropdown]
        [SerializeField] private string m_type;
        [SerializeField] private UpdateSubSystemInsertion m_insertion = UpdateSubSystemInsertion.InsideBottom;

        public string Target { get { return m_target; } set { m_target = value; } }
        public string Type { get { return m_type; } set { m_type = value; } }
        public UpdateSubSystemInsertion Insertion { get { return m_insertion; } set { m_insertion = value; } }

        protected override IUpdateSystemDescription OnBuild()
        {
            var targetSystemType = System.Type.GetType(m_target, true);
            var systemType = System.Type.GetType(m_type, true);

            return new UpdateSystemDescription(targetSystemType, systemType, m_insertion);
        }
    }
}
