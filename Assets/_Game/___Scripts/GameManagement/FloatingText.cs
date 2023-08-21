using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public Animator animator;
    public TextMeshPro floatingText;

    public string text;
    public Color color;
    public float liveTime;
    public int animId;

    public void InstantiateText()
    {
        animator = GetComponentInChildren<Animator>();
        floatingText = GetComponentInChildren<TextMeshPro>();

        floatingText.text = text;
        floatingText.color = color;

        animator.SetTrigger(animId.ToString());

        Destroy(gameObject, liveTime);
        /// Disable and reuse for optimisation?
    }
}
