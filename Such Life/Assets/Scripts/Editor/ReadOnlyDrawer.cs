using UnityEngine;
using UnityEditor;

/* Base codes provided by: Dan Pos - Game Dev Tutorials!*/

/// <summary>
/// This class contain custom drawer for ReadOnly attribute
/// </summary>
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    /// <summary>
    /// Unity method for drawing GUI in Editor
    /// </summary>
    /// <param name="position">Position.</param>
    /// <param name="property">Property.</param>
    /// <param name="label">Label.</param>

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //saving previous GUI enabled valuje
        var previousGUIState = GUI.enabled;
        //Disabling edit for property
        GUI.enabled = false;
        //Drawing property
        EditorGUI.PropertyField(position, property, label);
        //Setting old GUI enabled value
        GUI.enabled = previousGUIState;
    }
}
