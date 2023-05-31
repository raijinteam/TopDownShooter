using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotAblitiesManager : MonoBehaviour
{
    public static SlotAblitiesManager instance;


    public AbilitesInventoryProperty[] all_AbilitesInventoryItems;
    public int currentMaterialCount;
    public int currentEquippmentSelectedIndex;

    public int maxLevel;
    private void Awake()
    {
        instance = this;
    }

    public bool hasEnoughMaterialsForUpgrade(int _slotIndex)
    {
        if (currentMaterialCount >= all_AbilitesInventoryItems[_slotIndex].requireMaterialToLevelUp[all_AbilitesInventoryItems[_slotIndex].currentLevel])
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

        if (DataManager.instance.coins >= all_AbilitesInventoryItems[_slotIndex].requireCoinsToUpgrade)
        {
            return true;
        }

        return false;
    }


    public void UpgradeEquipnent(int _itemIndex)
    {
        currentMaterialCount -= all_AbilitesInventoryItems[_itemIndex].requireMaterialToLevelUp[all_AbilitesInventoryItems[_itemIndex].currentLevel];
        DataManager.instance.coins -= all_AbilitesInventoryItems[_itemIndex].requireCoinsToUpgrade;
        all_AbilitesInventoryItems[_itemIndex].currentFirerate += all_AbilitesInventoryItems[_itemIndex].firerateIncrease;
        all_AbilitesInventoryItems[_itemIndex].currentLevel++;
    }
}
