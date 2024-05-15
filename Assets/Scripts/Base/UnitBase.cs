using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    public Collider2D collider2d;

    private void Start()
    {
        if (collider2d == null)
        {
            collider2d = GetComponent<Collider2D>();
            if (collider2d == null)
            {
                collider2d = GetComponentInChildren<Collider2D>();
            }
        }
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
