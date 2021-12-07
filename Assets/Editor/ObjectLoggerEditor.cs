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

        if(GUILayout.Button("Start"))
        {
            var component = target as ObjectLogger;
            component.Begin();
        }
    }
}
