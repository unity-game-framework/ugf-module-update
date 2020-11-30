using System;
using UGF.Builder.Runtime;
using UGF.Update.Runtime;
using UnityEngine;

namespace UGF.Module.Update.Runtime
{
    public abstract class UpdateGroupAssetBase : BuilderAsset<IUpdateGroup>
    {
        [SerializeField] private string m_name;

        public string Name { get { return m_name; } set { m_name = value; } }

        protected override IUpdateGroup OnBuild()
        {
            IUpdateCollection collection = OnBuildCollection();

            if (collection == null) throw new ArgumentNullException(nameof(collection), "Update collection can not be null.");

            return OnBuild(collection);
        }

        protected abstract IUpdateCollection OnBuildCollection();
        protected abstract IUpdateGroup OnBuild(IUpdateCollection collection);
    }
}
