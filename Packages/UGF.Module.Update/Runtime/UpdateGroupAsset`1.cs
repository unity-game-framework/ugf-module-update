using System;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.IMGUI.Types;
using UGF.Update.Runtime;
using UnityEngine;

namespace UGF.Module.Update.Runtime
{
    public abstract class UpdateGroupAsset<TItem, TDescription> : UpdateGroupAsset
        where TItem : class
        where TDescription : class, IUpdateGroupDescription
    {
        [UpdateSystemTypeDropdown]
        [SerializeField] private TypeReference<object> m_systemType;

        public TypeReference<object> SystemType { get { return m_systemType; } set { m_systemType = value; } }

        protected override IUpdateGroupDescription OnBuildDescription()
        {
            Type type = m_systemType.Get();

            return new UpdateGroupDescription(type);
        }

        protected override IUpdateGroup OnBuild(IUpdateCollection collection, IUpdateGroupDescription description, IApplication application)
        {
            return OnBuild((IUpdateCollection<TItem>)collection, (TDescription)description, application);
        }

        protected virtual IUpdateGroup OnBuild(IUpdateCollection<TItem> collection, TDescription description, IApplication application)
        {
            return new UpdateGroupDescribed<TItem, TDescription>(collection, description);
        }
    }
}
