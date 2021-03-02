using System;
using UGF.EditorTools.Runtime.IMGUI.Types;
using UGF.Update.Runtime;
using UnityEngine;

namespace UGF.Module.Update.Runtime
{
    public abstract class UpdateGroupAsset<TItem, TDescription> : UpdateGroupAssetBase
        where TItem : class
        where TDescription : class, IUpdateGroupDescription
    {
        [SerializeField] private string m_name;
        [UpdateSystemTypeDropdown]
        [SerializeField] private TypeReference<object> m_systemType;

        public string Name { get { return m_name; } set { m_name = value; } }
        public TypeReference<object> SystemType { get { return m_systemType; } set { m_systemType = value; } }

        protected override IUpdateGroupDescription OnBuildDescription()
        {
            Type type = m_systemType.Get();

            return new UpdateGroupDescription(type);
        }

        protected override IUpdateGroup OnBuild(IUpdateCollection collection, IUpdateGroupDescription description)
        {
            return OnBuild((IUpdateCollection<TItem>)collection, (TDescription)description);
        }

        protected virtual IUpdateGroup OnBuild(IUpdateCollection<TItem> collection, TDescription description)
        {
            return new UpdateGroupDescribed<TItem, TDescription>(m_name, collection, description);
        }
    }
}
