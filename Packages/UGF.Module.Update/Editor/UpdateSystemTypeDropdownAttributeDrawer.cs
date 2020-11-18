using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.Module.Update.Runtime;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.Update.Editor
{
    [CustomPropertyDrawer(typeof(UpdateSystemTypeDropdownAttribute), true)]
    internal class UpdateSystemTypeDropdownAttributeDrawer : PropertyDrawerTyped<UpdateSystemTypeDropdownAttribute>
    {
        public UpdateSystemTypeDropdownAttributeDrawer() : base(SerializedPropertyType.String)
        {
        }

        protected override void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
        }
    }
}
