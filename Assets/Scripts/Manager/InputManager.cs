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

    private void Start()
    {
        myInput = new MyInput();
        myInput.Enable();
        player = GameManager.Instance.GetPlayer();
    }

    private void Update()
    {
        //Debug.Log(isBuilding);
    }

    private void Building_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        player.playerController.BuildingStart();
    }

    private void Building_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        player.playerController.BuildingCancel();
    }

    private void Upgrade_started(InputAction.CallbackContext obj)
    {
        //建筑升级
        currentUpgradeBuilding = player.GetOverBuilding();
        UpgradeBase upgrade = currentUpgradeBuilding.GetComponent<UpgradeBase>();
        Debug.Log(upgrade);
        upgrade?.DoUpgrade();
    }

    private void Upgrade_canceled(InputAction.CallbackContext obj)
    {
        //取消建筑升级
        currentUpgradeBuilding = player.GetOverBuilding();
        UpgradeBase upgrade = currentUpgradeBuilding.GetComponent<UpgradeBase>();
        upgrade?.CancelUpgrade();
    }

    public void AddBuildingEventHandler()
    {
        RemoveSpaceEventHandlers();
        myInput.Player.Building.started += Building_started;
        myInput.Player.Building.canceled += Building_canceled;
    }

    public void AddUpgradeEventHandler()
    {
        myInput.Player.Building.started += Upgrade_started;
        myInput.Player.Building.canceled += Upgrade_canceled;
    }

    public void RemoveSpaceEventHandlers()
    {
        myInput.Player.Building.started -= Building_started;
        myInput.Player.Building.canceled -= Building_canceled;
        myInput.Player.Building.started -= Upgrade_started;
        myInput.Player.Building.canceled -= Upgrade_canceled;
    }
}
