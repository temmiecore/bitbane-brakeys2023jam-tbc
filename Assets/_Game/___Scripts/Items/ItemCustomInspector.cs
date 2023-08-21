using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Item), true)]
public class ItemCustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Item weaponScript = (Item)target;
        if (GUILayout.Button("Level-Up Item"))
        {
            weaponScript.LevelUp();
        }
        if (GUILayout.Button("Apply Item Effect"))
        {
            weaponScript.ModifyParameter();
        }
    }
}
