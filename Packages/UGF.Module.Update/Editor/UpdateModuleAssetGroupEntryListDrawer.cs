using System;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.Update.Editor
{
    internal class UpdateModuleAssetGroupEntryListDrawer : ReorderableListDrawer
    {
        public UpdateModuleAssetGroupEntryListDrawer(SerializedProperty serializedProperty) : base(serializedProperty)
        {
        }

        protected override void OnDrawElementContent(Rect position, SerializedProperty serializedProperty, int index, bool isActive, bool isFocused)
        {
            SerializedProperty propertySubSystemType = serializedProperty.FindPropertyRelative("m_subSystemType");
            SerializedProperty propertySubSystemTypeValue = propertySubSystemType.FindPropertyRelative("m_value");
            SerializedProperty propertyGroup = serializedProperty.FindPropertyRelative("m_group");

            float height = EditorGUIUtility.singleLineHeight;
            float space = EditorGUIUtility.standardVerticalSpacing;

            var rectFoldout = new Rect(position.x, position.y, position.width, height);
            var rectSubSystemType = new Rect(position.x, rectFoldout.yMax + space, position.width, height);
            var rectGroup = new Rect(position.x, rectSubSystemType.yMax + space, position.width, height);

            string contentFoldout = GetSystemName(propertySubSystemTypeValue);

            serializedProperty.isExpanded = EditorGUI.Foldout(rectFoldout, serializedProperty.isExpanded, contentFoldout, true);

            if (serializedProperty.isExpanded)
            {
                using (new IndentIncrementScope(1))
                {
                    EditorGUI.PropertyField(rectSubSystemType, propertySubSystemType);
                    EditorGUI.PropertyField(rectGroup, propertyGroup);
                }
            }
        }

        private static string GetSystemName(SerializedProperty serializedProperty)
        {
            string value = serializedProperty.stringValue;

            if (!string.IsNullOrEmpty(value))
            {
                var type = Type.GetType(value);

                return type != null ? type.Name : "Missing";
            }

            return "None";
        }
    }
}
