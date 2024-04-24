using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionTimerUI : MonoBehaviour
{
    [SerializeField] private IConstructionAble construction;
    private Image constructionProgressImage;

    private void Awake()
    {
        constructionProgressImage = transform.Find("mask").Find("image").GetComponent<Image>();
        constructionProgressImage.fillAmount = 0;
    }

    private void Update()
    {
        if (construction != null)
        {
            constructionProgressImage.fillAmount = construction.GetConstructionTimerNormalized();
        }
    }

    public void SetConstructionAble(IConstructionAble construction)
    {
        this.construction = construction;
    }
}
