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
    [Header("Floating Text Object")]
    public FloatingText floatingTextObject;

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
