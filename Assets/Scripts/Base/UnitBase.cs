using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        OnStart();
    }

    public virtual void OnStart()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        OnUpdate();
    }

    public virtual void OnUpdate()
    {

    }

    //public void AddBuff(BuffBase buff)
    //{

    //}
}
