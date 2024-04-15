using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpendResource : MonoBehaviour
{
    [SerializeField] private Transform mask;

    public void Init()
    {
        mask.localScale = Vector3.zero;
    }

    public void SetMaskScale(float amount)
    {
        amount = Mathf.Clamp01(amount);
        mask.localScale = new Vector3(amount, amount, 1);
    }
}
