using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInventoryState : State
{
    private InventoryScreenController inventoryScreen;

    public override void Enter(GlobalStateMachine host)
    {
        base.Enter(host);
        SpawnInventory();
        host.ChangeState(nextState);
    }

    private void SpawnInventory()
    {
        inventoryScreen = (InventoryScreenController)GlobalEventManager.TriggerOnSpawnEvent(ScreenType.Inventory);
        inventoryScreen.Init(new InventoryStruct { slotCount = GameData.Instance.inventoryCount });
    }

    public override void Exit()
    {
        base.Exit();
    }
}
