using System;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.Update.Editor
{
    internal class UpdateModuleAssetEditorSystemEntryListDrawer : ReorderableListDrawer
    {
        public UpdateModuleAssetEditorSystemEntryListDrawer(SerializedProperty serializedProperty) : base(serializedProperty)
        {
        }

        protected override void OnDrawElementContent(Rect position, SerializedProperty serializedProperty, int index, bool isActive, bool isFocused)
        {
            SerializedProperty propertyTargetSystemType = serializedProperty.FindPropertyRelative("m_targetSystemType");
            SerializedProperty propertyTargetSystemTypeValue = propertyTargetSystemType.FindPropertyRelative("m_value");
            SerializedProperty propertySystemType = serializedProperty.FindPropertyRelative("m_systemType");
            SerializedProperty propertySystemTypeValue = propertySystemType.FindPropertyRelative("m_value");
            SerializedProperty propertyInsertion = serializedProperty.FindPropertyRelative("m_insertion");

            float height = EditorGUIUtility.singleLineHeight;
            float space = EditorGUIUtility.standardVerticalSpacing;

            var rectFoldout = new Rect(position.x, position.y, position.width, height);
            var rectTargetSystemType = new Rect(position.x, rectFoldout.yMax + space, position.width, height);
            var rectSystemType = new Rect(position.x, rectTargetSystemType.yMax + space, position.width, height);
            var rectInsertion = new Rect(position.x, rectSystemType.yMax + space, position.width, height);

            string targetSystemTypeName = GetSystemName(propertyTargetSystemTypeValue);
            string systemTypeName = GetSystemName(propertySystemTypeValue);
            string contentFoldout = $"{targetSystemTypeName}/{systemTypeName}";

            serializedProperty.isExpanded = EditorGUI.Foldout(rectFoldout, serializedProperty.isExpanded, contentFoldout, true);

            if (serializedProperty.isExpanded)
            {
                using (new IndentIncrementScope(1))
                {
                    EditorGUI.PropertyField(rectTargetSystemType, propertyTargetSystemType);
                    EditorGUI.PropertyField(rectSystemType, propertySystemType);
                    EditorGUI.PropertyField(rectInsertion, propertyInsertion);
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
