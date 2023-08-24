using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public CanvasGroup mainCanvas;
    public CanvasGroup optionsCanvas;

    public void OpenMainWindow()
    {
        mainCanvas.alpha = 1f; mainCanvas.blocksRaycasts = true;
        optionsCanvas.alpha = 0f; optionsCanvas.blocksRaycasts = false;
    }

    public void OpenOptionsWindow()
    {
        mainCanvas.alpha = 0f; mainCanvas.blocksRaycasts = false;
        optionsCanvas.alpha = 1f; optionsCanvas.blocksRaycasts = true;
    }

    public void StartStoryMode()
    {
        SceneManager.LoadScene("StoryLvl1");
    }
}
