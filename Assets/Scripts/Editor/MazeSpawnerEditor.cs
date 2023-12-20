using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MazeSpawner))]
public class MazeSpawnerEditor : Editor
{
    private MazeSpawner spawner;

    private void OnEnable()
    {
        spawner = (MazeSpawner)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Generate"))
        {
            spawner.ClearMaze();
            spawner.SpawnMaze();
        }
    }
    
}
