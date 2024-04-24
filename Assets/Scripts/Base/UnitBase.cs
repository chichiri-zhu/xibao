using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    private void Start()
    {
        OnStart();
    }

    public virtual void OnStart()
    {

    }

    private void Update()
    {
        OnUpdate();
    }

    public virtual void OnUpdate()
    {

    }

    public virtual bool CanLookFor()
    {
        return true;
    }

    //public void AddBuff(BuffBase buff)
    //{

    //}
}
