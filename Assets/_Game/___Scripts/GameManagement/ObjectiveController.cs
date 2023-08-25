using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveController : MonoBehaviour
{
    public int level;
    public TextMeshProUGUI objectiveText;
    public TextMeshProUGUI objectiveCountText;

    public int objectiveCount;
    public int objectiveCountMax;

    private bool isFinished;

    private void Start()
    {
        isFinished = false;
        InitializeObjective();
    }

    public void AddToObjectiveCount()
    {
        objectiveCount++;
        objectiveCountText.text = objectiveCount + "/" + objectiveCountMax;
        if (objectiveCount >= objectiveCountMax)
            OnObjectiveCompletion();
    }

    private void InitializeObjective()
    {
        switch (level)
        {
            default: { break; }
            case 1:
                {
                    objectiveText.text = "Defeat 50 enemies";
                    objectiveCountText.text = "0/50";
                    objectiveCount = 0; objectiveCountMax = 50;
                    break;
                }
            case 2:
                {
                    objectiveText.text = "Find and defeat 10 enemies, inflicted by the virus";
                    objectiveCountText.text = "0/10";
                    objectiveCount = 0; objectiveCountMax = 10;
                    break;
                }
            case 3:
                {
                    objectiveText.text = "Run to the portal!";
                    objectiveCountText.text = "";
                    objectiveCount = 0; objectiveCountMax = 1;
                    break;
                }
            case 4:
                {
                    objectiveText.text = "This is it. Defeat the virus!";
                    objectiveCountText.text = "";
                    objectiveCount = 0; objectiveCountMax = 1;
                    break;
                }
        }
    }

    private void OnObjectiveCompletion()
    {
        if (isFinished)
            return;

        Vector2 portalPosition = new Vector2(GameManager.Instance.playerMover.transform.position.x, GameManager.Instance.playerMover.transform.position.y)
            + Random.insideUnitCircle.normalized * 25f;

        Instantiate(GameManager.Instance.portalPrefab, portalPosition, Quaternion.identity);
        Instantiate(GameManager.Instance.portalArrowPrefab, Vector3.zero, Quaternion.identity);

        objectiveText.text = "Find the portal to a deeper level!";
        objectiveCountText.text = "";

        isFinished = true;
    }


}
