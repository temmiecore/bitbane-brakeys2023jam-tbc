using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenController : MonoBehaviour
{
    
    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
