using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTransitionUI : MonoBehaviour, IConstructionAble
{
    [SerializeField] private ConstructionTimerUI constructionTimer;
    private float timer;
    private float timerMax = 5f;
    //private Coroutine coroutine;
    private bool isHandle;
    private Player player;

    private void Start()
    {
        player = GameManager.Instance.GetPlayer();
        if (constructionTimer != null)
        {
            constructionTimer.SetConstructionAble(this);
        }
        Hide();
    }

    public void ToBattle()
    {
        Show();
        player.actionCoroutine = StartCoroutine(_ToBattleHandle());
    }

    public void Cancel()
    {
        if(isHandle)
        {
            player.CancelAction();
            Hide();
        }
    }

    private IEnumerator _ToBattleHandle()
    {
        isHandle = true;
        timer = 0f;
        while(timer < timerMax)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        Debug.Log("to battle");
        isHandle = false;
        GameManager.Instance.PrepareToBattle();
        Hide();
        //yield return null;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public float GetConstructionTimerNormalized()
    {
        return 1 - timer / timerMax;
    }
}
