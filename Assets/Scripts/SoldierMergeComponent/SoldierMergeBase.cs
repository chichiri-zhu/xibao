using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MergeInterface
{
    public void OnMerge();
    public void OnRemove();
}

public class SoldierMergeBase : MonoBehaviour, MergeInterface
{
    public void Start()
    {
        OnMerge();
    }

    public virtual void OnMerge()
    {

    }

    public virtual void OnRemove()
    {

    }


    public void OnDestroy()
    {
        OnRemove();
    }
}
