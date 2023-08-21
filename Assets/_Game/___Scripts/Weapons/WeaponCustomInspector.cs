using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(IWeapon), true)]
public class WeaponCustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        IWeapon weaponScript = (IWeapon)target;
        if (GUILayout.Button("Level-Up Weapon"))
        {
            weaponScript.LevelUp();
        }
    }
}
