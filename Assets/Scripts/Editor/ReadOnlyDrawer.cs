using ServiceLocator.Item;
using UnityEditor;
using UnityEngine;

namespace ServiceLocator.EditorTools
{
    [CustomPropertyDrawer(typeof(ItemScriptableObject.ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Disable editing in the Inspector
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label);
            GUI.enabled = true; // Re-enable GUI for other fields
        }
    }
}