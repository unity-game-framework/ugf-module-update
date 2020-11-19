using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UGF.EditorTools.Editor.IMGUI.Types;
using UGF.Module.Update.Runtime;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.Update.Editor
{
    [CustomPropertyDrawer(typeof(UpdateSystemTypeDropdownAttribute), true)]
    internal class UpdateSystemTypeDropdownAttributeDrawer : TypesDropdownAttributePropertyDrawer<UpdateSystemTypeDropdownAttribute>
    {
        public UpdateSystemTypeDropdownAttributeDrawer() : base(SerializedPropertyType.Generic)
        {
        }

        protected override DropdownDrawer<DropdownItem<Type>> OnCreateDrawer()
        {
            return new TypesDropdownDrawer(GetItems);
        }

        protected override void OnDrawProperty(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty propertyValue = property.FindPropertyRelative("m_value");

            if (propertyValue != null)
            {
                base.OnDrawProperty(position, propertyValue, label);
            }
            else
            {
                OnDrawPropertyDefault(position, property, label);
            }
        }

        public override float GetPropertyHeight(SerializedProperty serializedProperty, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        protected override void OnGetItems(ICollection<DropdownItem<Type>> items)
        {
            items.Add(NoneItem);

            TypesDropdownEditorUtility.GetTypeItems(items, Attribute.TargetType, Attribute.DisplayFullPath, Attribute.DisplayAssemblyName);
            TypesDropdownEditorUtility.GetTypeItems(items, OnValidate, Attribute.DisplayFullPath, Attribute.DisplayAssemblyName);
        }

        private bool OnValidate(Type type)
        {
            return type.Namespace == "UnityEngine.PlayerLoop";
        }
    }
}
