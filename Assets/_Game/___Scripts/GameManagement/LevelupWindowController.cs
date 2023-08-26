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
    public List<Button> buttons;

    private List<bool> isNew;
    private List<ICollectable> items;

    private List<ICollectable> bufferList;
    private int totalWeight;

    private int reopenTime;

    private void Start()
    {
        isNew = new List<bool> { true, true, true };
        items = new List<ICollectable> { null, null, null };
    }

    public void ChooseItem(int cell)
    {
        if (isNew[cell])
        {
            ICollectable newItem = Instantiate(items[cell], GameManager.Instance.itemParent);
            GameManager.Instance.playerCollectables.Add(newItem);
            GameManager.Instance.inGameUIController.UpdateIcons();

        }
        else
        {
            ICollectable itemOnPlayer = GetItemOnPlayer(items[cell].itemId);
            itemOnPlayer.LevelUp();
            if (itemOnPlayer.level >= itemOnPlayer.maxLevel)
                GameManager.Instance.collectables.Remove(items[cell]);
        }

        CloseWindow();
    }

    public void UpdateWindow()
    {
        icons[0].sprite = null; icons[1].sprite = null; icons[2].sprite = null;
        descriptions[0].text = ""; descriptions[1].text = ""; descriptions[2].text = "";
        buttons[0].enabled = false; buttons[1].enabled = false; buttons[2].enabled = false;


        int weaponCount = 0;
        foreach (ICollectable item in GameManager.Instance.playerCollectables)
        {
            if (item is IWeapon)
                weaponCount++;
        }

        if (weaponCount == 4)
        {
            foreach (ICollectable item in GameManager.Instance.collectables)
            {
                if (item is IWeapon && !GameManager.Instance.playerCollectables.Contains(item))
                    GameManager.Instance.collectables.Remove(item);
            }
        }

        totalWeight = 0;
        bufferList = new List<ICollectable>(GameManager.Instance.collectables);
        foreach (ICollectable item in bufferList)
            totalWeight += item.weight;

        int i = 0;
        int bufferListSize = bufferList.Count;

        while (i < Mathf.Min(3, bufferListSize))
        {
            ICollectable item = SelectRandomItem();

            icons[i].sprite = item.icon;
            icons[i].SetNativeSize();

            ICollectable itemOnPlayer = GetItemOnPlayer(item.itemId);

            if (itemOnPlayer != null)
            {
                if (itemOnPlayer.level >= itemOnPlayer.maxLevel)
                    continue;

                isNew[i] = false;
                descriptions[i].text = itemOnPlayer.levelDescriptions[itemOnPlayer.level + 1];
            }
            else
            {
                isNew[i] = true;
                descriptions[i].text = item.levelDescriptions[0];
            }

            buttons[i].enabled = true;
            items[i] = item;
            i++;
        }
    }

    public ICollectable GetItemOnPlayer(int id)
    { return GameManager.Instance.playerCollectables.Find(x => x.itemId == id); }

    public ICollectable SelectRandomItem()
    {
        int randomWeight = Random.Range(0, totalWeight);
        int cumulativeWeight = 0;

        foreach (ICollectable item in bufferList)
        {
            cumulativeWeight += item.weight;

            if (cumulativeWeight > randomWeight)
            { bufferList.Remove(item); totalWeight -= item.weight; return item; }
        }

        return null;
    }

    public void OpenWindow()
    {
        if (canvasGroup.alpha == 1f)
        {
            reopenTime++;
            return;
        }

        UpdateWindow();
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        Time.timeScale = 0;
    }

    public void CloseWindow()
    {
        Time.timeScale = 1;
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;

        if (reopenTime > 0)
        {
            OpenWindow();
            reopenTime--;
        }

    }
}
