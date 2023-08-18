using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotAblitiesManager : MonoBehaviour
{
    public static SlotAblitiesManager instance;


    public AbilitesInventoryProperty[] all_AbilitesInventoryItems;
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
        Debug.Log("Increase In Ablities");
        DataManager.instance.SetAblitiesCurrentMat(currentMaterialCount);
    }

    public void SetEqData(int value)
    {
        DataManager.instance.SetAblitiesCurrentMat(value);
        for (int i = 0; i < all_AbilitesInventoryItems.Length; i++)
        {
            DataManager.instance.SetAbiitiesEQLevel(i, value);
        }
        GetEQData();
    }


    public void GetEQData()
    {
        currentMaterialCount = DataManager.instance.GetAblitiesMaterials();
        for (int i = 0; i < all_AbilitesInventoryItems.Length; i++)
        {
            all_AbilitesInventoryItems[i].currentLevel = DataManager.instance.GetAblitiesCurrentLevel(i);
        }
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

        int currentLevel = all_AbilitesInventoryItems[_slotIndex].currentLevel;
        if (DataManager.instance.coins >= all_AbilitesInventoryItems[_slotIndex].requireCoinsToUpgrade[currentLevel])
        {
            return true;
        }

        return false;
    }


    public void UpgradeEquipnent(int _itemIndex)
    {
        int currentLevel = all_AbilitesInventoryItems[_itemIndex].currentLevel;
        currentMaterialCount -= all_AbilitesInventoryItems[_itemIndex].requireMaterialToLevelUp[all_AbilitesInventoryItems[_itemIndex].currentLevel];
        DataManager.instance.DecreaseCoins(all_AbilitesInventoryItems[_itemIndex].requireCoinsToUpgrade[currentLevel]);
        all_AbilitesInventoryItems[_itemIndex].currentLevel++;
        DataManager.instance.SetAbiitiesEQLevel(_itemIndex, all_AbilitesInventoryItems[_itemIndex].currentLevel);
    }
}
