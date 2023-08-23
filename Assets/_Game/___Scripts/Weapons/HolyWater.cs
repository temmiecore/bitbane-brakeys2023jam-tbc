using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyWater : MonoBehaviour
{
    public float damage;
    public float knockbackStrength;
    public float radius;
    public float duration;

    public HolyWaterBroken brokenBottlePrefab;


    public void SetupProjectile(Weapon_HolyWater weapon)
    {
        damage = weapon.damage;
        knockbackStrength = weapon.knockbackStrength;
        radius = weapon.areaRadius;
        duration = weapon.areaDuration;
    }

    public void BreakBottle()
    {
        StartCoroutine(BreakBottleCoroutine());
    }

    private IEnumerator BreakBottleCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        HolyWaterBroken script = Instantiate(brokenBottlePrefab, transform.position, transform.rotation);
        script.SetUpArea(this);
        Destroy(gameObject);
    }
}

