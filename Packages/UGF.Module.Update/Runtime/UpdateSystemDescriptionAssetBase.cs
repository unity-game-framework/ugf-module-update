using UnityEngine;

namespace UGF.Module.Update.Runtime
{
    public abstract class UpdateSystemDescriptionAssetBase : ScriptableObject
    {
        public T Build<T>() where T : class, IUpdateSystemDescription
        {
            return (T)OnBuild();
        }

        public IUpdateSystemDescription Build()
        {
            return OnBuild();
        }

        protected abstract IUpdateSystemDescription OnBuild();
    }
}
