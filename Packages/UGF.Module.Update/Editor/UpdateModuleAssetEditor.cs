using UGF.EditorTools.Editor.IMGUI.AssetReferences;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.Update.Runtime;
using UnityEditor;

namespace UGF.Module.Update.Editor
{
    [CustomEditor(typeof(UpdateModuleAsset), true)]
    internal class UpdateModuleAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyScript;
        private AssetReferenceListDrawer m_listSystems;
        private AssetReferenceListDrawer m_listGroups;

        private void OnEnable()
        {
            m_propertyScript = serializedObject.FindProperty("m_Script");
            m_listSystems = new AssetReferenceListDrawer(serializedObject.FindProperty("m_systems"));
            m_listGroups = new AssetReferenceListDrawer(serializedObject.FindProperty("m_groups"));

            m_listSystems.Enable();
            m_listGroups.Enable();
        }

        private void OnDisable()
        {
            m_listSystems.Disable();
            m_listGroups.Disable();
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
            }
        }
    }
}
