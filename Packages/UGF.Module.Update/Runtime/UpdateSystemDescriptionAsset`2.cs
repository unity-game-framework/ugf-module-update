using System;
using UGF.Update.Runtime;
using UnityEngine;

namespace UGF.Module.Update.Runtime
{
    public class UpdateSystemDescriptionAsset<TTarget, TType> : UpdateSystemDescriptionAssetBase
    {
        [SerializeField] private UpdateSubSystemInsertion m_insertion = UpdateSubSystemInsertion.InsideBottom;

        public UpdateSubSystemInsertion Insertion { get { return m_insertion; } set { m_insertion = value; } }

        protected override IUpdateSystemDescription OnGetDescription()
        {
            Type targetSystemType = typeof(TTarget);
            Type systemType = typeof(TType);

            return new UpdateSystemDescription(targetSystemType, systemType, m_insertion);
        }
    }
}
