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

    [Header("References")]
    public PlayerController playerController;
    public PlayerMover playerMover;
    public PlayerParameters playerParameters;
}
