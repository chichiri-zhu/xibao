using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : SingleBase<LevelManager>
{
    private int level;

    public event EventHandler OnLevelUp;

    public override void OnAwake()
    {
        level = 1;
    }

    public int GetLevel()
    {
        return level;
    }

    public void LevelUp()
    {
        level++;
        OnLevelUp?.Invoke(this, EventArgs.Empty);
    }
}
