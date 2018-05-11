using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Health))]
public class HealthEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty currentHealth = serializedObject.FindProperty("currentHealth");
        SerializedProperty maxHealth = serializedObject.FindProperty("maxHealth");

        currentHealth.floatValue = EditorGUILayout.Slider(currentHealth.floatValue, 0, maxHealth.floatValue);
        EditorGUILayout.PropertyField(maxHealth);

        serializedObject.ApplyModifiedProperties();

        Health health = (Health)target;

        if (GUILayout.Button("20 Damage"))
        {
            health.Damage(20);
        }
    }
}
