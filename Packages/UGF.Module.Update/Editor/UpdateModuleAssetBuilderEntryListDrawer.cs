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

            var rectGroup = new Rect(position.x, position.y, position.width, height);
            var rectBuilder = new Rect(position.x, rectGroup.yMax + space, position.width, height);

            using (new IndentIncrementScope(-1))
            {
                EditorGUI.PropertyField(rectGroup, propertyGroup);
                EditorGUI.PropertyField(rectBuilder, propertyBuilder);
            }
        }

        protected override float OnElementHeightContent(SerializedProperty serializedProperty, int index)
        {
            float height = EditorGUIUtility.singleLineHeight;
            float space = EditorGUIUtility.standardVerticalSpacing;

            return height * 2F + space;
        }
    }
}
