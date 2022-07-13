using UGF.EditorTools.Editor.Ids;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.Update.Editor
{
    internal class UpdateModuleAssetBuilderEntryListDrawer : ReorderableListDrawer
    {
        public UpdateModuleAssetBuilderEntryListDrawer(SerializedProperty serializedProperty) : base(serializedProperty)
        {
        }

        protected override void OnDrawElementContent(Rect position, SerializedProperty serializedProperty, int index, bool isActive, bool isFocused)
        {
            SerializedProperty propertyGroup = serializedProperty.FindPropertyRelative("m_group");
            SerializedProperty propertyBuilder = serializedProperty.FindPropertyRelative("m_builder");

            float height = EditorGUIUtility.singleLineHeight;
            float space = EditorGUIUtility.standardVerticalSpacing;

            var rectFoldout = new Rect(position.x, position.y, position.width, height);
            var rectGroup = new Rect(position.x, rectFoldout.yMax + space, position.width, height);
            var rectBuilder = new Rect(position.x, rectGroup.yMax + space, position.width, height);

            string contentFoldout = GetGroupName(propertyGroup);

            serializedProperty.isExpanded = EditorGUI.Foldout(rectFoldout, serializedProperty.isExpanded, contentFoldout, true);

            if (serializedProperty.isExpanded)
            {
                using (new IndentIncrementScope(1))
                {
                    EditorGUI.PropertyField(rectGroup, propertyGroup);
                    EditorGUI.PropertyField(rectBuilder, propertyBuilder);
                }
            }
        }

        private static string GetGroupName(SerializedProperty serializedProperty)
        {
            string value = GlobalIdEditorUtility.GetGuidFromProperty(serializedProperty);

            if (!string.IsNullOrEmpty(value))
            {
                string path = AssetDatabase.GUIDToAssetPath(value);
                var asset = AssetDatabase.LoadAssetAtPath<Object>(path);

                return asset != null ? asset.name : "Missing";
            }

            return "None";
        }
    }
}
