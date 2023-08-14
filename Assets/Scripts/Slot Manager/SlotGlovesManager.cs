using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotGlovesManager : MonoBehaviour
{
    public static SlotGlovesManager instance;


    public GlovesEquipmentProperty[] all_GlovesInventoryItems;
    public int activeIndex;
    public int currentMaterialCount;
    public int currentEquippmentSelectedIndex;

    public int maxLevel;
    private void Awake()
    {
        instance = this;
    }


    public void SetEqData(int value)
    {
        DataManager.instance.SetGloveurrentMat(value);
        for (int i = 0; i < all_GlovesInventoryItems.Length; i++)
        {
            DataManager.instance.SetGloveEQLevel(i, value);
        }
        GetEQData();
    }


    public void GetEQData()
    {
        currentMaterialCount = DataManager.instance.GetGloveMaterials();
        for (int i = 0; i < all_GlovesInventoryItems.Length; i++)
        {
            all_GlovesInventoryItems[i].currentLevel = DataManager.instance.GetGloveCurrentLevel(i);
        }
    }


    public bool hasEnoughMaterialsForUpgrade(int _slotIndex)
    {
        if (currentMaterialCount >= all_GlovesInventoryItems[_slotIndex].requireMaterialToLevelUp[all_GlovesInventoryItems[_slotIndex].currentLevel])
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

        if (DataManager.instance.coins >= all_GlovesInventoryItems[_slotIndex].requireCoinsToUpgrade)
        {
            return true;
        }

        return false;
    }


    public void UpgradeEquipnent(int _itemIndex)
    {
        currentMaterialCount -= all_GlovesInventoryItems[_itemIndex].requireMaterialToLevelUp[all_GlovesInventoryItems[_itemIndex].currentLevel];
        DataManager.instance.coins -= all_GlovesInventoryItems[_itemIndex].requireCoinsToUpgrade;
        all_GlovesInventoryItems[_itemIndex].currentDamage += all_GlovesInventoryItems[_itemIndex].damageIncrease;
        all_GlovesInventoryItems[_itemIndex].currentLevel++;
    }
}
