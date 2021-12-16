using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SerialPortLogger))]
public class SerialPortLoggerEditor : Editor
{
    // Start is called before the first frame update
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var component = target as SerialPortLogger;


        GUILayout.Label($"Records {component.Records}");

        if (!component.Running)
        {
            if (GUILayout.Button("Start"))
            {
                component.Begin();
            }
        }
        else
        {
            if (GUILayout.Button("Stop"))
            {
                component.OnDestroy();
            }
        }

        GUILayout.Label(component.LastEntry);
    }
}
