using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The possible inventory related events
/// </summary>
public enum TowerMergeEventType { Click }

/// <summary>
/// Inventory events are used throughout the Inventory Engine to let other interested classes know that something happened to an inventory.  
/// </summary>
public struct TowerMergeEvent
{
	/// the type of event
	public TowerMergeEventType towerMergeEventType;
	/// the slot involved in the event
	public TowerMergeSoldierUI towerMergeSoldier;

	public TowerMergeEvent(TowerMergeEventType eventType, TowerMergeSoldierUI soldier)
	{
		towerMergeEventType = eventType;
		towerMergeSoldier = soldier;
	}

	static TowerMergeEvent e;
	public static void Trigger(TowerMergeEventType eventType, TowerMergeSoldierUI soldier)
	{
		e.towerMergeEventType = eventType;
		e.towerMergeSoldier = soldier;
		MMEventManager.TriggerEvent(e);
	}
}
