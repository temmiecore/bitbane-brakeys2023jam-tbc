using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelupWindowController : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    public List<Image> icons;
    public List<TextMeshProUGUI> descriptions;
    private List<bool> isNew;
    private List<ICollectable> items;

    public List<ICollectable> bufferList;
    private int totalWeight;

    private void Start()
    {
        isNew = new List<bool> { true, true, true };
        items = new List<ICollectable> { null, null, null };
    }

    public void ChooseItem(int cell)
    {
        if (isNew[cell])
            Instantiate(items[cell], GameManager.Instance.itemParent);
        else
            items[cell].LevelUp();

        CloseWindow();
    }

    public void UpdateWindow()
    {
        totalWeight = 0;
        bufferList = new List<ICollectable>(GameManager.Instance.collectables);
        foreach (ICollectable item in bufferList)
            totalWeight += item.weight;

        for (int i = 0; i < 3; i++)
        {
            ICollectable item = SelectRandomItem();

            icons[i].sprite = item.icon;
            icons[i].SetNativeSize();

            /// This is not working 
            if (GameManager.Instance.playerCollectables.Contains(item))
            {
                isNew[i] = false;
                /// FIGURE OUT A WAY TO CHECK IF ITEM IS POSSESED BY PLAYER AND GET REFERENCE TO IT
            }
            else
            {
                isNew[i] = true;
                descriptions[i].text = item.levelDescriptions[0];
            }


            items[i] = item;
        }
    }

    public ICollectable SelectRandomItem()
    {
        int randomWeight = Random.Range(0, totalWeight);
        int cumulativeWeight = 0;

        Debug.Log("||| "+totalWeight+" - "+randomWeight);

        foreach (ICollectable item in bufferList)
        {
            cumulativeWeight += item.weight;

            Debug.Log(cumulativeWeight);

            if (cumulativeWeight > randomWeight)
            { bufferList.Remove(item); totalWeight -= item.weight; return item; }
        }

        Debug.Log("Baaad");
        return null;
    }

    public void OpenWindow()
    {
        UpdateWindow();
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void CloseWindow()
    {
        UpdateWindow();
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
    }
}
