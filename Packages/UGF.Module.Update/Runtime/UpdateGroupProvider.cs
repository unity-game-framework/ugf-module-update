using System;
using System.Collections.Generic;
using UGF.Description.Runtime;
using UGF.Logs.Runtime;
using UGF.RuntimeTools.Runtime.Providers;
using UGF.Update.Runtime;

namespace UGF.Module.Update.Runtime
{
    public class UpdateGroupProvider : Provider<string, IUpdateGroup>
    {
        public IUpdateProvider UpdateProvider { get; }

        public UpdateGroupProvider(IUpdateProvider updateProvider) : this(updateProvider, EqualityComparer<string>.Default)
        {
        }

        public UpdateGroupProvider(IUpdateProvider updateProvider, IEqualityComparer<string> comparer) : base(comparer)
        {
            UpdateProvider = updateProvider ?? throw new ArgumentNullException(nameof(updateProvider));
        }

        protected override void OnAdd(string id, IUpdateGroup entry)
        {
            if (!(entry is IDescribed<IUpdateGroupDescription> described)) throw new ArgumentException("Entry must be of 'IDescribed<IUpdateGroupDescription>' type.", nameof(entry));

            UpdateProvider.Add(described.Description.SystemType, entry);

            base.OnAdd(id, entry);

            Log.Debug("Add update group", new
            {
                id,
                entry.Name,
                described.Description.SystemType
            });
        }

        protected override bool OnRemove(string id, IUpdateGroup entry)
        {
            UpdateProvider.Remove(entry);

            Log.Debug("Remove update group", new
            {
                id,
                entry.Name
            });

            return base.OnRemove(id, entry);
        }

        protected override void OnClear()
        {
            foreach (KeyValuePair<string, IUpdateGroup> pair in this)
            {
                UpdateProvider.Remove(pair.Value);
            }

            base.OnClear();
        }
    }
}
