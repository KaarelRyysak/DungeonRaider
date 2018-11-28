using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(AttributeEnemies))]
public class AttributeInGameText : Editor {

    void onInspectorGUI()
    {
        EditorGUILayout.Separator();
        GUILayout.Label("HELLO WORLD!");
        GUILayout.Button("this does nothing");

        
    }
}
