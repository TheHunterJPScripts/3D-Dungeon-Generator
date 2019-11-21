using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DungeonGenerator))]
public class DungeonGeneratorInspector : Editor
{
    DungeonGenerator data;

    private void OnEnable() {
        data = (DungeonGenerator)target;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        EditorGUILayout.Space();

        if(GUILayout.Button("Generate Next Room")) {
            data.GenerateRoom();
        }
        EditorGUILayout.Space();

        if (GUILayout.Button("Generate Coroutine")) {
            data.GenerateCoroutine();
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Open Editor")) {
            RoomWindow.ShowWindow();
        }
    }
}
