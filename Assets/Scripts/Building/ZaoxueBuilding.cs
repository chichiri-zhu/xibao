using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZaoxueBuilding : BuildingBase
{
    private int redballAmount = 0;
    private List<RedBall> redballList;
    private Dictionary<Vector2, RedBall> redballDic;

    [SerializeField] private List<Transform> redballTransforms;

    protected override void OnStart()
    {
        GameManager.Instance.OnBattleEnd += Instance_OnBattleEnd;
        redballDic = new Dictionary<Vector2, RedBall>();
        foreach (var item in redballTransforms)
        {
            redballDic.Add(item.transform.position, null);
        }
    }

    private void Instance_OnBattleEnd(object sender, System.EventArgs e)
    {
        AddOneRedball();
    }

    public void AddOneRedball()
    {
        if(redballAmount >= 6)
        {
            return;
        }

        Vector2 redballPos = GetRandomNullKey(redballDic);
        Debug.Log(redballPos);
        if(redballPos != Vector2.zero)
        {
            RedBall redBall = RedBall.Create(redballPos, transform);
            redballDic[redballPos] = redBall;
            redballAmount++;
            Debug.Log("add redball");
        }
    }

    private Vector2 GetRandomNullKey(Dictionary<Vector2, RedBall> dictionary)
    {
        List<Vector2> nullKeys = new List<Vector2>();

        foreach (KeyValuePair<Vector2, RedBall> pair in dictionary)
        {
            if (pair.Value == null)
            {
                nullKeys.Add(pair.Key);
            }
        }

        if (nullKeys.Count > 0)
        {
            int randomIndex = Random.Range(0, nullKeys.Count);
            return nullKeys[randomIndex];
        }

        return Vector2.zero;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameManager.Instance.OnBattleEnd -= Instance_OnBattleEnd;
    }
}
