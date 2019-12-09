using UGF.Application.Runtime;
using UGF.Update.Runtime;
using UnityEngine;

namespace UGF.Module.Update.Runtime
{
    [CreateAssetMenu(menuName = "UGF/Module.Update/UpdateModuleInfo", order = 2000)]
    public class UpdateModuleInfoAsset : ApplicationModuleInfoAsset<IUpdateModule>
    {
        [SerializeField] private string m_initializationGroupName = "UpdateModule.Initialization";
        [SerializeField] private string m_earlyUpdateGroupName = "UpdateModule.EarlyUpdate";
        [SerializeField] private string m_fixedUpdateGroupName = "UpdateModule.FixedUpdate";
        [SerializeField] private string m_preUpdateGroupName = "UpdateModule.PreUpdate";
        [SerializeField] private string m_updateGroupName = "UpdateModule.Update";
        [SerializeField] private string m_preLateUpdateGroupName = "UpdateModule.PreLateUpdate";
        [SerializeField] private string m_postLateUpdateGroupName = "UpdateModule.PostLateUpdate";

        public string InitializationGroupName { get { return m_initializationGroupName; } set { m_initializationGroupName = value; } }
        public string EarlyUpdateGroupName { get { return m_earlyUpdateGroupName; } set { m_earlyUpdateGroupName = value; } }
        public string FixedUpdateGroupName { get { return m_fixedUpdateGroupName; } set { m_fixedUpdateGroupName = value; } }
        public string PreUpdateGroupName { get { return m_preUpdateGroupName; } set { m_preUpdateGroupName = value; } }
        public string UpdateGroupName { get { return m_updateGroupName; } set { m_updateGroupName = value; } }
        public string PreLateUpdateGroupName { get { return m_preLateUpdateGroupName; } set { m_preLateUpdateGroupName = value; } }
        public string PostLateUpdateGroupName { get { return m_postLateUpdateGroupName; } set { m_postLateUpdateGroupName = value; } }

        public IUpdateModuleDescription GetDescription()
        {
            return new UpdateModuleDescription
            {
                InitializationGroupName = m_initializationGroupName,
                EarlyUpdateGroupName = m_earlyUpdateGroupName,
                FixedUpdateGroupName = m_fixedUpdateGroupName,
                PreUpdateGroupName = m_preUpdateGroupName,
                UpdateGroupName = m_updateGroupName,
                PreLateUpdateGroupName = m_preUpdateGroupName,
                PostLateUpdateGroupName = m_postLateUpdateGroupName
            };
        }

        protected override IApplicationModule OnBuild(IApplication application)
        {
            var updateLoop = new UpdateLoopUnity();
            var provider = new UpdateProvider(updateLoop);
            IUpdateModuleDescription description = GetDescription();

            return new UpdateModule(provider, description);
        }
    }
}
