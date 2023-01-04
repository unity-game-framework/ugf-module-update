using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.Update.Editor
{
    internal class UpdateModuleAssetSystemListDrawer : ReorderableListDrawer
    {
        public UpdateModuleAssetSystemListDrawer(SerializedProperty serializedProperty) : base(serializedProperty)
        {
        }

        protected override void OnDrawElementContent(Rect position, SerializedProperty serializedProperty, int index, bool isActive, bool isFocused)
        {
            SerializedProperty propertyTargetSystemType = serializedProperty.FindPropertyRelative("m_targetSystemType");
            SerializedProperty propertySystemType = serializedProperty.FindPropertyRelative("m_systemType");
            SerializedProperty propertyInsertion = serializedProperty.FindPropertyRelative("m_insertion");

            float height = EditorGUIUtility.singleLineHeight;
            float space = EditorGUIUtility.standardVerticalSpacing;

            var rectTargetSystemType = new Rect(position.x, position.y, position.width, height);
            var rectSystemType = new Rect(position.x, rectTargetSystemType.yMax + space, position.width, height);
            var rectInsertion = new Rect(position.x, rectSystemType.yMax + space, position.width, height);

            using (new IndentIncrementScope(-1))
            {
                EditorGUI.PropertyField(rectTargetSystemType, propertyTargetSystemType);
                EditorGUI.PropertyField(rectSystemType, propertySystemType);
                EditorGUI.PropertyField(rectInsertion, propertyInsertion);
            }
        }

        protected override float OnElementHeightContent(SerializedProperty serializedProperty, int index)
        {
            return EditorGUIUtility.singleLineHeight * 3F + EditorGUIUtility.standardVerticalSpacing * 2F;
        }
    }
}
