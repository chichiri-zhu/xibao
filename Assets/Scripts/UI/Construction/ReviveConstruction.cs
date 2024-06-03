using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveConstruction : MonoBehaviour, IConstructionAble
{
    [SerializeField] private ConstructionTimerUI constructionTimer;
    [SerializeField] private Player player;

    private void Start()
    {
        constructionTimer.SetConstructionAble(this);
    }

    private void Update()
    {
        if(player.GetSoldierStatus() == SoldierStatus.Death)
        {
            constructionTimer.gameObject.SetActive(true);
        }
        else
        {
            constructionTimer.gameObject.SetActive(false);
        }
    }

    public float GetConstructionTimerNormalized()
    {
        return 1 - player.GetReviveNormalized();
    }
}
