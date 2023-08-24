using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is used for getting user input and using it
/// to invoke different player-related actions, other that movement.
/// </summary>
[RequireComponent(typeof(PlayerParameters))]
public class PlayerController : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            OpenPauseMenu();
    }

    private void OpenPauseMenu()
    {
        GameManager.Instance.pauseMenuController.OpenPauseMenu();
    }
}
