using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        /// ADD DontDestroyOnLoad FOR EVERY NON-DESTRUCTABLE OBJECTS
    }

    [Header("References", order = 0)]
    [Header("Player", order = 1)]
    public PlayerController playerController;
    public PlayerMover playerMover;
    public PlayerParameters playerParameters;
    public Transform weaponParent;
    public Transform itemParent;

    [Header("Floating Text Object")]
    public FloatingText floatingTextObject;

    [Header("Items/Weapons List")]
    public List<ICollectable> collectables;

    [Header("Items/Weapons already on Player")]
    public List<ICollectable> playerCollectables;

    [Header("Levelup Window Controller")]
    public LevelupWindowController levelupWindowController;

    [Header("In Game UI Controller")]
    public InGameUIController inGameUIController;

    [Header("Pause Menu Controller")]
    public PauseMenuController pauseMenuController;

    private void Start()
    {
        collectables.Sort((x,y) => x.weight.CompareTo(y.weight));
    }

    public void InstantiateFloatingText(string text, Color color, float liveTime, int animId, Transform target)
    {
        FloatingText floatingTextInstance = Instantiate(floatingTextObject, new Vector3(target.position.x, target.position.y + 0.1f, 0), target.rotation);

        floatingTextInstance.text = text;
        floatingTextInstance.color = color;
        floatingTextInstance.liveTime = liveTime;
        floatingTextInstance.animId = animId;

        floatingTextInstance.InstantiateText();

    }
}
