using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ICollectable : MonoBehaviour
{
    public int itemId;

    [Header("Level")]
    public int level;
    public int maxLevel;
    public List<string> levelDescriptions; /// First item in List will be description of the weapon.

    [Header("Icon sprite")]
    public Sprite icon;
    public Sprite oneBitIcon;

    [Header("Item weight")]
    public int weight;

    public abstract void LevelUp();
}
