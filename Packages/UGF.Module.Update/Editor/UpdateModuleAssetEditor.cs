using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.Update.Runtime;
using UnityEditor;

namespace UGF.Module.Update.Editor
{
    [CustomEditor(typeof(UpdateModuleAsset), true)]
    internal class UpdateModuleAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyScript;
        private UpdateModuleAssetEditorSystemEntryListDrawer m_listSystems;
        private UpdateModuleAssetGroupEntryListDrawer m_listGroups;
        private UpdateModuleAssetBuilderEntryListDrawer m_listSubGroups;
        private UpdateModuleAssetBuilderEntryListDrawer m_listEntries;

        private void OnEnable()
        {
            m_propertyScript = serializedObject.FindProperty("m_Script");
            m_listSystems = new UpdateModuleAssetEditorSystemEntryListDrawer(serializedObject.FindProperty("m_systems"));
            m_listGroups = new UpdateModuleAssetGroupEntryListDrawer(serializedObject.FindProperty("m_groups"));
            m_listSubGroups = new UpdateModuleAssetBuilderEntryListDrawer(serializedObject.FindProperty("m_subGroups"));
            m_listEntries = new UpdateModuleAssetBuilderEntryListDrawer(serializedObject.FindProperty("m_entries"));

            m_listSystems.Enable();
            m_listGroups.Enable();
            m_listSubGroups.Enable();
            m_listEntries.Enable();
        }

        private void OnDisable()
        {
            m_listSystems.Disable();
            m_listGroups.Disable();
            m_listSubGroups.Disable();
            m_listEntries.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                using (new EditorGUI.DisabledScope(true))
                {
                    EditorGUILayout.PropertyField(m_propertyScript);
                }

                m_listSystems.DrawGUILayout();
                m_listGroups.DrawGUILayout();
                m_listSubGroups.DrawGUILayout();
                m_listEntries.DrawGUILayout();
            }
        }
    }
}
