using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    public TextMeshProUGUI tmp;

    public void Start()
    {

    }

    public IEnumerator TypeWriterCoroutine(string text, float waitTime)
    {
        for (var i = 0; i < text.Length + 1; i++)
        {
            tmp.text = text.Substring(0, i);

            yield return new WaitForSecondsRealtime(waitTime);
        }
    }

    public void TypeWriter1()
    {
        StartCoroutine(TypeWriterCoroutine("...", 0.4f));
    }

    public void TypeWriter2()
    {
        StartCoroutine(TypeWriterCoroutine("To be continued.", 0.05f));
    }

    public void TypeWriter3()
    {
        StartCoroutine(TypeWriterCoroutine("As you could probably tell, this game is VERY unfinished, lol.", 0.05f));
    }

    public void TypeWriter4()
    {
        StartCoroutine(TypeWriterCoroutine("And if you encountered any bugs, I probably know about them. But feel free to tell me in the comments anyway.", 0.05f));
    }

    public void TypeWriter5()
    {
        StartCoroutine(TypeWriterCoroutine("Making a game in a week is harder than I thought...", 0.05f));
    }

    public void TypeWriter6()
    {
        StartCoroutine(TypeWriterCoroutine("I'm talking too much. Thank you for playing!", 0.05f));
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
