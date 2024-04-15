using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceManager : SingleBase<ResourceManager>
{
    private int gold = 10;
    public event EventHandler OnGoldUpdate;

    public int GetGold()
    {
        return gold;
    }

    public void AddGold(int amount)
    {
        gold += amount;
        gold = Mathf.Clamp(gold, 0, 9999);
        OnGoldUpdate?.Invoke(this, EventArgs.Empty);
    }

    public void SpendGold(int amount)
    {
        gold -= amount;
        gold = Mathf.Clamp(gold, 0, 9999);
        OnGoldUpdate?.Invoke(this, EventArgs.Empty);
    }
}
