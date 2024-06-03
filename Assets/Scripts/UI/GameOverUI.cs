using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : UIbase
{
    public override void OnStart()
    {
        GameManager.Instance.OnGameOver += Instance_OnGameOver;    
    }

    private void Instance_OnGameOver(object sender, System.EventArgs e)
    {
        Show();
    }

    public override void HandleEsc()
    {
        return;
    }
}
