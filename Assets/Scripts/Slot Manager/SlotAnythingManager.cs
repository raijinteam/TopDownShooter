using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotAnythingManager : MonoBehaviour
{
    public static SlotAnythingManager instance;


    public AnythingInventoryProperty[] all_AnythingInventoryItems;
    public int currentMaterialCount;
    public int currentEquippmentSelectedIndex;

    public int maxLevel;
    private void Awake()
    {
        instance = this;
    }

    public bool hasEnoughMaterialsForUpgrade(int _slotIndex)
    {
        if (currentMaterialCount >= all_AnythingInventoryItems[_slotIndex].requireMaterialToLevelUp[all_AnythingInventoryItems[_slotIndex].currentLevel])
        {
            return true;
        }

        return false;
    }

    public bool hasEnoughCoinsForUpgrade(int _slotIndex)
    {
        //if (currentMaterialCount >= all_HeadInventory[_slotIndex].c[all_HeadInventory[_slotIndex].currentLevel])
        //{
        //    return true;
        //}

        if (DataManager.instance.coins >= all_AnythingInventoryItems[_slotIndex].requireCoinsToUpgrade)
        {
            return true;
        }

        return false;
    }

    public void UpgradeEquipnent(int _itemIndex)
    {
        currentMaterialCount -= all_AnythingInventoryItems[_itemIndex].requireMaterialToLevelUp[all_AnythingInventoryItems[_itemIndex].currentLevel];
        DataManager.instance.coins -= all_AnythingInventoryItems[_itemIndex].requireCoinsToUpgrade;
        all_AnythingInventoryItems[_itemIndex].currentFirerate += all_AnythingInventoryItems[_itemIndex].firerateIncrease;
        all_AnythingInventoryItems[_itemIndex].currentLevel++;
    }
}
