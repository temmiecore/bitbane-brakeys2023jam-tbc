using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            switch (GameManager.Instance.objectiveController.level)
            {
                default: { break; }
                case 1:
                    {
                        SceneManager.LoadScene("StoryLvl2");
                        break;
                    }
                case 2:
                    {
                        SceneManager.LoadScene("StoryLvl3");
                        break;
                    }
                case 3:
                    {
                        SceneManager.LoadScene("StoryLvl4");
                        break;
                    }
                case 4:
                    {
                        break;
                    }

            }
        }
    }
}
