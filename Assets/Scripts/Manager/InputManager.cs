using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : SingleBase<InputManager>
{
    private MyInput myInput;
    private Player player;

    private bool isBuilding = false;
    private BuildingPlace currentBuildingPlace;
    private BuildingBase currentUpgradeBuilding;

    public Action EscAction;
    public Action QAction;

    public override void OnAwake()
    {
        myInput = new MyInput();
        myInput.Enable();
        myInput.Player.Esc.performed += Esc_performed;
        myInput.Player.Merge.performed += Merge_performed;
    }

    private void Start()
    {
        player = GameManager.Instance.GetPlayer();
        AddToBattleEventHandler();
    }

    private void Esc_performed(InputAction.CallbackContext obj)
    {
        //EscAction?.Invoke();
        UIbase[] openUIs = CanvasManager.Instance.gameObject.GetComponentsInChildren<UIbase>();
        bool isOtherUIOpen = false;
        foreach (UIbase ui in openUIs)
        {
            Debug.Log(ui);
            //判断是否是暂停界面
            if (ui.gameObject == CanvasManager.Instance.pauseUI.gameObject)
            {
                continue;
            }
            if (ui.IsShow())
            {
                isOtherUIOpen = true;
                ui.HandleEsc();
            }
        }

        if (!isOtherUIOpen)
        {
            GameManager.Instance.EscHandle();
        }
    }

    private void Merge_performed(InputAction.CallbackContext obj)
    {
        QAction?.Invoke();
    }

    private void Update()
    {
        //Debug.Log(isBuilding);
    }

    private void Building_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if(GameManager.Instance.GetGameStatus() == GameStatus.Prepare)
        {
            player.playerController.BuildingStart();
        }
    }

    private void Building_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (GameManager.Instance.GetGameStatus() == GameStatus.Prepare)
        {
            player.playerController.BuildingCancel();
        }
    }

    private void Upgrade_started(InputAction.CallbackContext obj)
    {
        if (GameManager.Instance.GetGameStatus() == GameStatus.Prepare)
        {
            //建筑升级
            currentUpgradeBuilding = player.GetOverBuilding();
            UpgradeBase upgrade = currentUpgradeBuilding.GetComponent<UpgradeBase>();
            Debug.Log(upgrade);
            upgrade?.DoUpgrade();
        }
    }

    private void Upgrade_canceled(InputAction.CallbackContext obj)
    {
        if (GameManager.Instance.GetGameStatus() == GameStatus.Prepare)
        {
            //取消建筑升级
            currentUpgradeBuilding = player.GetOverBuilding();
            UpgradeBase upgrade = currentUpgradeBuilding.GetComponent<UpgradeBase>();
            upgrade?.CancelUpgrade();
        }
    }

    private void ToBattle_started(InputAction.CallbackContext obj)
    {
        //进入战斗
        if(GameManager.Instance.GetGameStatus() == GameStatus.Prepare)
        {
            CanvasManager.Instance.battleTransitionUI.ToBattle();
        }
    }

    private void ToBattle_canceled(InputAction.CallbackContext obj)
    {
        //取消进入战斗
        if (GameManager.Instance.GetGameStatus() == GameStatus.Prepare)
        {
            CanvasManager.Instance.battleTransitionUI.Cancel();
        }
    }

    public void AddBuildingEventHandler()
    {
        RemoveSpaceEventHandlers();
        myInput.Player.Building.started += Building_started;
        myInput.Player.Building.canceled += Building_canceled;
    }

    public void AddUpgradeEventHandler()
    {
        RemoveSpaceEventHandlers();
        myInput.Player.Building.started += Upgrade_started;
        myInput.Player.Building.canceled += Upgrade_canceled;
    }

    public void AddToBattleEventHandler()
    {
        RemoveSpaceEventHandlers();
        myInput.Player.Building.started += ToBattle_started;
        myInput.Player.Building.canceled += ToBattle_canceled;
    }

    public void RemoveSpaceEventHandlers()
    {
        myInput.Player.Building.started -= Building_started;
        myInput.Player.Building.canceled -= Building_canceled;
        myInput.Player.Building.started -= Upgrade_started;
        myInput.Player.Building.canceled -= Upgrade_canceled;
        myInput.Player.Building.started -= ToBattle_started;
        myInput.Player.Building.canceled -= ToBattle_canceled;
    }
}
