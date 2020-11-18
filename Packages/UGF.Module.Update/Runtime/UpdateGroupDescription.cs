using System;
using UGF.Update.Runtime;

namespace UGF.Module.Update.Runtime
{
    public class UpdateGroupDescription : UpdateGroupDescriptionBase
    {
        public UpdateGroupDescription(string name, Type systemType) : base(name, systemType)
        {
        }

        protected override IUpdateGroup OnCreateGroup(string name, IUpdateCollection collection)
        {
            return new UpdateGroup(name, collection);
        }

        protected override IUpdateCollection OnCreateUpdateCollection()
        {
            return new UpdateSet<IUpdateHandler>();
        }
    }
}
