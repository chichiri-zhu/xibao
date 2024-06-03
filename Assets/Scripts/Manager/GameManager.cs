using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using MoreMountains.Tools;

public class GameManager : SingleBase<GameManager>
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject mainBuilding;
    [SerializeField] private GameStatus gameStatus = GameStatus.Padding;

    public int towerUpgradeDec = 0;

    public bool canPause = true;
    private bool isPause = false;

    public event EventHandler OnPrepareStart;
    public event EventHandler OnPrepareEnd;
    public event EventHandler OnBattleStart;
    public event EventHandler OnBattleEnd;
    public event EventHandler OnGameStatusUpdate;
    public event EventHandler OnPause;
    public event EventHandler OnRegain;
    public event EventHandler OnGameOver;

    public MyInput myInput;

    public override void OnAwake()
    {
        gameStatus = GameStatus.Prepare;
        Time.timeScale = 1;
        //myInput = new MyInput();
        //myInput.Enable();
        //myInput.Player.Esc.performed += ctx =>
        //{
            
        //};
    }

    private void Start()
    {
        InputManager.Instance.EscAction = EscHandle;
    }

    public void EscHandle()
    {
        ////判断是否有其他界面打开
        //UIbase[] openUIs = GetComponentsInChildren<UIbase>();
        //bool isOtherUIOpen = false;
        //foreach (UIbase ui in openUIs)
        //{
        //    //判断是否是暂停界面
        //    if(ui.gameObject == CanvasManager.Instance.pauseUI)
        //    {
        //        continue;
        //    }
        //    if (ui.IsShow())
        //    {
        //        isOtherUIOpen = true;
        //        break;
        //    }
        //}

        //if (isOtherUIOpen)
        //{
        //    return;
        //}
        if (IsPause())
        {
            Regain();
            CanvasManager.Instance.pauseUI.GetComponent<UIbase>().Hide();
        }
        else
        {
            Debug.Log("gamemanager:" + canPause);
            if (CanPause())
            {
                Pause();
                CanvasManager.Instance.pauseUI.GetComponent<UIbase>().Show();
            }
        }
    }

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

    public bool CanPause()
    {
        return canPause;
    }

    public bool IsPause()
    {
        return isPause;
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

    public void Restart()
    {
        //SceneManager.LoadScene(0, LoadSceneMode.Additive);
        MMSceneLoadingManager.LoadScene("Game");
    }

    public void ToMenu()
    {
        MMSceneLoadingManager.LoadScene("Menu");
    }

    public GameStatus GetGameStatus()
    {
        return gameStatus;
    }

    public void GameOver()
    {
        OnGameOver?.Invoke(this, EventArgs.Empty);
        UpdateGameStatus(GameStatus.GameOver);
    }

    public void Clear()
    {
        //增加资源
        int addResourceAmount = (int)Math.Ceiling((double)(LevelManager.Instance.GetLevel() + 2) / 10);
        Debug.Log(addResourceAmount);
        if(addResourceAmount > 0)
        {
            ResourceManager.Instance.AddGold(addResourceAmount);
        }
        //关卡升级
        LevelManager.Instance.LevelUp();
        BattleToPrepare();
    }
}
