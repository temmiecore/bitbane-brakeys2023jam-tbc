using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIController : MonoBehaviour
{
    public List<Image> itemIcons;
    public List<Image> weaponIcons;

    public Sprite defaultItemIcon;
    public Sprite defaultWeaponIcon;

    private int iconId, weaponId;

    public Transform healthBar;
    public Transform xpBar;

    private void Update()
    {
        CalculateHPBar();
        CalculateXPBar();
    }

    public void CalculateHPBar()
    {
        healthBar.localScale = new Vector3(1, GameManager.Instance.playerParameters.hp / GameManager.Instance.playerParameters.maxHP, 1);
    }

    public void CalculateXPBar()
    {
        xpBar.localScale = new Vector3((float)GameManager.Instance.playerParameters.xp / GameManager.Instance.playerParameters.requiredXP, 1, 1);
    }

    public void UpdateIcons()
    {
        iconId = 0; weaponId = 0;

        foreach (Image image in itemIcons)
            image.sprite = defaultItemIcon;
        foreach (Image image in weaponIcons)
            image.sprite = defaultWeaponIcon;

        foreach (ICollectable collectable in GameManager.Instance.playerCollectables)
        {
            if (collectable is Item)
            {
                if (GameManager.Instance.objectiveController.level == 1)
                    itemIcons[iconId].sprite = collectable.icon;
                else
                    itemIcons[iconId].sprite = collectable.oneBitIcon;
                iconId++;
            }

            if (collectable is IWeapon)
            {
                if (GameManager.Instance.objectiveController.level == 1)
                {
                    weaponIcons[weaponId].sprite = collectable.icon;
                    weaponIcons[weaponId].SetNativeSize();
                    weaponIcons[weaponId].rectTransform.sizeDelta = new Vector2(30, 30);
                }
                else
                {
                    weaponIcons[weaponId].sprite = collectable.oneBitIcon;
                    weaponIcons[weaponId].SetNativeSize();
                    weaponIcons[weaponId].transform.localScale = new Vector3(2.5f, 2.5f, 1);
                }
                weaponId++;
            }
        }
    }
}
