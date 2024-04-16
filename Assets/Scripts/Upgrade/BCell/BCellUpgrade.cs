using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCellUpgrade : UpgradeBase
{
    [SerializeField] private List<ArmsSO> armsUpgradeList;
    public override IEnumerator _DoUpgrade()
    {
        yield return null;
    }
}
