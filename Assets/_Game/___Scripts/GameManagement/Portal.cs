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
                        GameManager.Instance.playerMover.transform.position = new Vector3(50, 10, 0);
                        FindObjectOfType<CreditsController>().GetComponent<Animator>().enabled = true;
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
