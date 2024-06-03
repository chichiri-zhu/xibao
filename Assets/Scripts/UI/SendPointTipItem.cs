using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendPointTipItem : MonoBehaviour
{
    public static SendPointTipItem Create(ArmsSO arms, int amount, Transform parentTransform)
    {
        Transform pfSendPointTipItem = Resources.Load<Transform>("pfSendPointTipItem");
        Transform sendPointItemTransform = Instantiate(pfSendPointTipItem, parentTransform);
        SendPointTipItem sendPointItem = sendPointItemTransform.GetComponent<SendPointTipItem>();
        sendPointItem.SetArms(arms);
        sendPointItem.SetAmount(amount);
        return sendPointItem;
    }

    [SerializeField] private Image icon;
    [SerializeField] private MyText amountText;
    private ArmsSO arms;
    private int amount = 0;

    public void SetArms(ArmsSO arms)
    {
        this.arms = arms;
        icon.sprite = arms.icon;
    }

    public void SetAmount(int amount)
    {
        this.amount = amount;
        amountText?.SetText(amount.ToString());
    }
}
