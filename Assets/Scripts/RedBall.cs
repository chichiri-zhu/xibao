using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBall : MonoBehaviour
{
    private int amount = 1;
    public static RedBall Create(Vector3 position, Transform parentTransform = null)
    {
        Transform pfRedBall = Resources.Load<Transform>("pfRedBall");
        Transform redballTransform = Instantiate(pfRedBall, position, Quaternion.identity);
        if(parentTransform != null)
        {
            redballTransform.SetParent(parentTransform);
        }

        RedBall redball = redballTransform.GetComponent<RedBall>();
        return redball;
    }

    private void Start()
    {
        GameManager.Instance.OnPrepareStart += Instance_OnPrepareStart;
    }

    private void Instance_OnPrepareStart(object sender, System.EventArgs e)
    {
        ResourceManager.Instance.AddGold(amount);
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnPrepareStart -= Instance_OnPrepareStart;
    }
}
