using UnityEngine;

namespace UGF.Module.Update.Runtime
{
    public abstract class UpdateSystemDescriptionAssetBase : ScriptableObject
    {
        public IUpdateSystemDescription GetDescription()
        {
            return OnGetDescription();
        }

        protected abstract IUpdateSystemDescription OnGetDescription();
    }
}
