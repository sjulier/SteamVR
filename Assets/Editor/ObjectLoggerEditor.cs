using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObjectLogger))]
public class ObjectLoggerEditor : Editor
{
    // Start is called before the first frame update
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var component = target as ObjectLogger;

        GUILayout.Label($"Records {component.Records}");

        if(GUILayout.Button("Start"))
        {
            component.Begin();
        }

        GUILayout.Label(component.LastEntry);
    }
}
