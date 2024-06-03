using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SendManager : SingleBase<SendManager>
{
    private List<SendPoint> pointList;
    private Coroutine coroutine;

    public override void OnAwake()
    {
        pointList = GetComponentsInChildren<SendPoint>().ToList();
    }

    private void Start()
    {
        GameManager.Instance.OnBattleStart += Instance_OnBattleStart;
    }

    private void Instance_OnBattleStart(object sender, System.EventArgs e)
    {
        coroutine = StartCoroutine(CheckClear());
    }

    private IEnumerator CheckClear()
    {
        yield return new WaitForSeconds(0.5f);
        bool isClear = false;
        while (!isClear)
        {
            bool flag = false;
            foreach (SendPoint point in pointList)
            {
                //Debug.Log(point.gameObject + ":" + point.isClear);
                if (!point.isClear)
                {
                    flag = true;
                    break;
                }
            }
            
            if (!flag)
            {
                isClear = true;
            }
            yield return null;
        }
        yield return null;
        //进入下个level
        GameManager.Instance.Clear();
    }
}
