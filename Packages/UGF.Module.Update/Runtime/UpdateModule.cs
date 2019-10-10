using UGF.Application.Runtime;
using UGF.Module.Update.Runtime.Handlers;
using UGF.Update.Runtime;
using Unity.Profiling;
using UnityEngine.LowLevel;
using PlayerLoops = UnityEngine.PlayerLoop;

namespace UGF.Module.Update.Runtime
{
    public class UpdateModule : ApplicationModuleBase, IUpdateModule
    {
        public IUpdateCollection<IPreUpdateHandler> PreUpdate { get { return m_preUpdate; } }
        public IUpdateCollection<IUpdateHandler> Update { get { return m_update; } }
        public IUpdateCollection<IFixedUpdateHandler> FixedUpdate { get { return m_fixedUpdate; } }
        public IUpdateCollection<IPostLateUpdateHandler> PostLateUpdate { get { return m_postLateUpdate; } }

        private readonly UpdateSetHandler<IPreUpdateHandler> m_preUpdate = new UpdateSetHandler<IPreUpdateHandler>(handler => handler.PreUpdate());
        private readonly UpdateSet<IUpdateHandler> m_update = new UpdateSet<IUpdateHandler>();
        private readonly UpdateSetHandler<IFixedUpdateHandler> m_fixedUpdate = new UpdateSetHandler<IFixedUpdateHandler>(handler => handler.OnFixedUpdate());
        private readonly UpdateSetHandler<IPostLateUpdateHandler> m_postLateUpdate = new UpdateSetHandler<IPostLateUpdateHandler>(handler => handler.OnPostLateUpdate());
        private static ProfilerMarker m_preUpdateMarker = new ProfilerMarker("UpdateModule.OnPreUpdate");
        private static ProfilerMarker m_updateMarker = new ProfilerMarker("UpdateModule.OnUpdate");
        private static ProfilerMarker m_fixedUpdateMarker = new ProfilerMarker("UpdateModule.OnFixedUpdate");
        private static ProfilerMarker m_postLateUpdateMarker = new ProfilerMarker("UpdateModule.OnPostLateUpdate");

        protected override void OnInitialize()
        {
            base.OnInitialize();

            PlayerLoopSystem playerLoop = PlayerLoop.GetCurrentPlayerLoop();

            UpdateUtility.TryAddUpdateFunction(playerLoop, typeof(PlayerLoops.PreUpdate), OnPreUpdate);
            UpdateUtility.TryAddUpdateFunction(playerLoop, typeof(PlayerLoops.Update), OnUpdate);
            UpdateUtility.TryAddUpdateFunction(playerLoop, typeof(PlayerLoops.FixedUpdate), OnFixedUpdate);
            UpdateUtility.TryAddUpdateFunction(playerLoop, typeof(PlayerLoops.PostLateUpdate), OnPostLateUpdate);

            PlayerLoop.SetPlayerLoop(playerLoop);
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            PlayerLoopSystem playerLoop = PlayerLoop.GetCurrentPlayerLoop();

            UpdateUtility.TryRemoveUpdateFunction(playerLoop, typeof(PlayerLoops.PreUpdate), OnPreUpdate);
            UpdateUtility.TryRemoveUpdateFunction(playerLoop, typeof(PlayerLoops.Update), OnUpdate);
            UpdateUtility.TryRemoveUpdateFunction(playerLoop, typeof(PlayerLoops.FixedUpdate), OnFixedUpdate);
            UpdateUtility.TryRemoveUpdateFunction(playerLoop, typeof(PlayerLoops.PostLateUpdate), OnPostLateUpdate);

            PlayerLoop.SetPlayerLoop(playerLoop);
        }

        private void OnPreUpdate()
        {
            m_preUpdateMarker.Begin();
            m_preUpdate.ApplyQueueAndUpdate();
            m_preUpdateMarker.End();
        }

        private void OnUpdate()
        {
            m_updateMarker.Begin();
            m_update.ApplyQueueAndUpdate();
            m_updateMarker.End();
        }

        private void OnFixedUpdate()
        {
            m_fixedUpdateMarker.Begin();
            m_fixedUpdate.ApplyQueueAndUpdate();
            m_fixedUpdateMarker.End();
        }

        private void OnPostLateUpdate()
        {
            m_postLateUpdateMarker.Begin();
            m_postLateUpdate.ApplyQueueAndUpdate();
            m_postLateUpdateMarker.End();
        }
    }
}
