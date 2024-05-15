using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruins : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.OnPrepareStart += Instance_OnPrepareStart;
    }

    private void Instance_OnPrepareStart(object sender, System.EventArgs e)
    {
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnPrepareStart -= Instance_OnPrepareStart;
    }
}
