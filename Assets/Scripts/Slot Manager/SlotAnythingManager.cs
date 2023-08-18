using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotAnythingManager : MonoBehaviour
{
    public static SlotAnythingManager instance;


    public AnythingInventoryProperty[] all_AnythingInventoryItems;
    public int activeIndex;
    public int currentMaterialCount;
    public int currentEquippmentSelectedIndex;

    public int maxLevel;
    private void Awake()
    {
        instance = this;
    }


    public void IncreaseMaterial(int amount)
    {
        currentMaterialCount += amount;
        Debug.Log("Increase In Anythig");
        DataManager.instance.SetAnythingCurrentMat(currentMaterialCount);
    }


    public void SetEqData(int value)
    {
        DataManager.instance.SetAnythingCurrentMat(value);
        for (int i = 0; i < all_AnythingInventoryItems.Length; i++)
        {
            DataManager.instance.SetAnythingEQLevel(i, value);
        }
        GetEQData();
    }


    public void GetEQData()
    {
        currentMaterialCount = DataManager.instance.GetAnythingMaterials();
        for (int i = 0; i < all_AnythingInventoryItems.Length; i++)
        {
            all_AnythingInventoryItems[i].currentLevel = DataManager.instance.GetAnythingCurrentLevel(i);
        }
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

        int currentLevel = all_AnythingInventoryItems[_slotIndex].currentLevel;

        if (DataManager.instance.coins >= all_AnythingInventoryItems[_slotIndex].requireCoinsToUpgrade[currentLevel])
        {
            return true;
        }

        return false;
    }

    public void UpgradeEquipnent(int _itemIndex)
    {
        int currentLevel = all_AnythingInventoryItems[_itemIndex].currentLevel;
        currentMaterialCount -= all_AnythingInventoryItems[_itemIndex].requireMaterialToLevelUp[all_AnythingInventoryItems[_itemIndex].currentLevel];
        DataManager.instance.DecreaseCoins(all_AnythingInventoryItems[_itemIndex].requireCoinsToUpgrade[currentLevel]);
        all_AnythingInventoryItems[_itemIndex].currentLevel++;
        DataManager.instance.SetAnythingEQLevel(_itemIndex, all_AnythingInventoryItems[_itemIndex].currentLevel);
    }
}
