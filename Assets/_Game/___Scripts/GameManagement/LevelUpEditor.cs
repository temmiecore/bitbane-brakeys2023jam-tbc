using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelupWindowController), true)]
public class LevelUpEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelupWindowController window = (LevelupWindowController)target;
        if (GUILayout.Button("Open window"))
        {
            window.OpenWindow();
        }
    }
}
