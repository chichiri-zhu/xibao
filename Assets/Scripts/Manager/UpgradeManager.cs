using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : SingleBase<UpgradeManager>
{
    public GameObject mainBuilding;
    private void Start()
    {
        mainBuilding = GameManager.Instance.GetMainBuilding();
    }
}
