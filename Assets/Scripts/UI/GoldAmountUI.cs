using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldAmountUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI amount;

    private void Start()
    {
        SetAmount();

        ResourceManager.Instance.OnGoldUpdate += Instance_OnGoldUpdate;
    }

    private void Instance_OnGoldUpdate(object sender, System.EventArgs e)
    {
        SetAmount();
    }

    private void SetAmount()
    {
        amount.SetText(ResourceManager.Instance.GetGold().ToString());
    }

    private void OnDestroy()
    {
        ResourceManager.Instance.OnGoldUpdate -= Instance_OnGoldUpdate;
    }
}
