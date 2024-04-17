using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : SingleBase<GameManager>
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject mainBuilding;
    [SerializeField] private GameStatus gameStatus = GameStatus.Padding;

    private bool isPause = false;

    public event EventHandler OnPrepareStart;
    public event EventHandler OnPrepareEnd;
    public event EventHandler OnBattleStart;
    public event EventHandler OnBattleEnd;
    public event EventHandler OnGameStatusUpdate;
    public event EventHandler OnPause;
    public event EventHandler OnRegain;

    public Player GetPlayer()
    {
        return player;
    }

    public void UpdateGameStatus(GameStatus newGameStatus)
    {
        gameStatus = newGameStatus;
        OnGameStatusUpdate?.Invoke(this, EventArgs.Empty);
    }

    public GameObject GetMainBuilding()
    {
        if(mainBuilding != null)
        {
            return mainBuilding;
        }
        else
        {
            return null; 
        }
    }

    public void BattleToPrepare()
    {
        OnBattleEnd?.Invoke(this, EventArgs.Empty);
        UpdateGameStatus(GameStatus.Prepare);
        OnPrepareStart?.Invoke(this, EventArgs.Empty);
    }

    public void PrepareToBattle()
    {
        OnPrepareEnd?.Invoke(this, EventArgs.Empty);
        UpdateGameStatus(GameStatus.Battle);
        OnBattleStart?.Invoke(this, EventArgs.Empty);
    }

    //暂停游戏
    public void Pause()
    {
        isPause = true;
        Time.timeScale = 0;
        OnPause?.Invoke(this, EventArgs.Empty);
    }

    //恢复游戏
    public void Regain()
    {
        if (isPause)
        {
            isPause = false;
            Time.timeScale = 1;
            OnRegain?.Invoke(this, EventArgs.Empty);
        }
    }
}
