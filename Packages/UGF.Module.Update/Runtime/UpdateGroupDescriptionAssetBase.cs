using UnityEngine;

namespace UGF.Module.Update.Runtime
{
    public abstract class UpdateGroupDescriptionAssetBase : ScriptableObject
    {
        public T Build<T>() where T : class, IUpdateGroupDescription
        {
            return (T)OnBuild();
        }

        public IUpdateGroupDescription Build()
        {
            return OnBuild();
        }

        protected abstract IUpdateGroupDescription OnBuild();
    }
}
