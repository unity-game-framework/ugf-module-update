using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.Update.Runtime;
using UnityEditor;

namespace UGF.Module.Update.Editor
{
    [CustomEditor(typeof(UpdateModuleAsset), true)]
    internal class UpdateModuleAssetEditor : UnityEditor.Editor
    {
        private UpdateModuleAssetSystemListDrawer m_listSystems;
        private ReorderableListKeyAndValueDrawer m_listGroups;
        private ReorderableListSelectionDrawerByPath m_listGroupsSelection;
        private ReorderableListKeyAndValueDrawer m_listSubGroups;
        private ReorderableListSelectionDrawerByPathGlobalId m_listSubGroupsSelectionGroup;
        private ReorderableListSelectionDrawerByPath m_listSubGroupsSelectionBuilder;
        private ReorderableListKeyAndValueDrawer m_listEntries;
        private ReorderableListSelectionDrawerByPathGlobalId m_listEntriesSelectionGroup;
        private ReorderableListSelectionDrawerByPath m_listEntriesSelectionBuilder;

        private void OnEnable()
        {
            m_listSystems = new UpdateModuleAssetSystemListDrawer(serializedObject.FindProperty("m_systems"));
            m_listGroups = new ReorderableListKeyAndValueDrawer(serializedObject.FindProperty("m_groups"), "m_subSystemType", "m_group");

            m_listGroupsSelection = new ReorderableListSelectionDrawerByPath(m_listGroups, "m_group.m_asset")
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listSubGroups = new ReorderableListKeyAndValueDrawer(serializedObject.FindProperty("m_subGroups"), "m_group", "m_builder");

            m_listSubGroupsSelectionGroup = new ReorderableListSelectionDrawerByPathGlobalId(m_listSubGroups, "m_group")
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listSubGroupsSelectionBuilder = new ReorderableListSelectionDrawerByPath(m_listSubGroups, "m_builder.m_asset")
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listEntries = new ReorderableListKeyAndValueDrawer(serializedObject.FindProperty("m_entries"), "m_group", "m_builder");

            m_listEntriesSelectionGroup = new ReorderableListSelectionDrawerByPathGlobalId(m_listSubGroups, "m_group")
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listEntriesSelectionBuilder = new ReorderableListSelectionDrawerByPath(m_listSubGroups, "m_builder.m_asset")
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listSystems.Enable();
            m_listGroups.Enable();
            m_listGroupsSelection.Enable();
            m_listSubGroups.Enable();
            m_listSubGroupsSelectionGroup.Enable();
            m_listSubGroupsSelectionBuilder.Enable();
            m_listEntries.Enable();
            m_listEntriesSelectionGroup.Enable();
            m_listEntriesSelectionBuilder.Enable();
        }

        private void OnDisable()
        {
            m_listSystems.Disable();
            m_listGroups.Disable();
            m_listGroupsSelection.Disable();
            m_listSubGroups.Disable();
            m_listSubGroupsSelectionGroup.Disable();
            m_listSubGroupsSelectionBuilder.Disable();
            m_listEntries.Disable();
            m_listEntriesSelectionGroup.Disable();
            m_listEntriesSelectionBuilder.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                m_listSystems.DrawGUILayout();
                m_listGroups.DrawGUILayout();
                m_listSubGroups.DrawGUILayout();
                m_listEntries.DrawGUILayout();

                m_listGroupsSelection.DrawGUILayout();
                m_listSubGroupsSelectionGroup.DrawGUILayout();
                m_listSubGroupsSelectionBuilder.DrawGUILayout();
                m_listEntriesSelectionGroup.DrawGUILayout();
                m_listEntriesSelectionBuilder.DrawGUILayout();
            }
        }
    }
}
