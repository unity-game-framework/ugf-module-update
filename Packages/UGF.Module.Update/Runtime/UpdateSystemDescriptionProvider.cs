using System;
using System.Collections.Generic;
using UGF.Logs.Runtime;
using UGF.RuntimeTools.Runtime.Providers;
using UGF.Update.Runtime;

namespace UGF.Module.Update.Runtime
{
    public class UpdateSystemDescriptionProvider : Provider<string, IUpdateSystemDescription>
    {
        public IUpdateLoop UpdateLoop { get; }

        public UpdateSystemDescriptionProvider(IUpdateLoop updateLoop) : this(updateLoop, EqualityComparer<string>.Default)
        {
        }

        public UpdateSystemDescriptionProvider(IUpdateLoop updateLoop, IEqualityComparer<string> comparer) : base(comparer)
        {
            UpdateLoop = updateLoop ?? throw new ArgumentNullException(nameof(updateLoop));
        }

        protected override void OnAdd(string id, IUpdateSystemDescription entry)
        {
            UpdateLoop.Add(entry.TargetSystemType, entry.SystemType, entry.Insertion);

            base.OnAdd(id, entry);

            Log.Debug("Add update system", new
            {
                id,
                entry.TargetSystemType,
                entry.SystemType,
                entry.Insertion
            });
        }

        protected override bool OnRemove(string id, IUpdateSystemDescription entry)
        {
            UpdateLoop.Remove(entry.SystemType);

            Log.Debug("Remove update system", new
            {
                id,
                entry.TargetSystemType,
                entry.SystemType,
                entry.Insertion
            });

            return base.OnRemove(id, entry);
        }

        protected override void OnClear()
        {
            foreach (KeyValuePair<string, IUpdateSystemDescription> pair in this)
            {
                UpdateLoop.Remove(pair.Value.SystemType);
            }

            base.OnClear();
        }
    }
}
