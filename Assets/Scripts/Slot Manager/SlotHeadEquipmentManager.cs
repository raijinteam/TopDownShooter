using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotHeadEquipmentManager : MonoBehaviour
{
    public static SlotHeadEquipmentManager instance;

    public HeadInventoryProperty[] all_HeadInventory;
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
        DataManager.instance.SetHeadCurrentMat(value);
        for (int i = 0; i < all_HeadInventory.Length; i++)
        {
            DataManager.instance.SetHeadEQLevel(i, value);
        }
        GetEQData();
    }

    public void GetEQData()
    {
        currentMaterialCount = DataManager.instance.GetHeadMaterials();
        for (int i = 0; i < all_HeadInventory.Length; i++)
        {
            all_HeadInventory[i].currentLevel = DataManager.instance.GetHeadCurrentLevel(i);
        }
    }

    public bool hasEnoughMaterialsForUpgrade(int _slotIndex)
    {
        if(currentMaterialCount >= all_HeadInventory[_slotIndex].requireMaterialToLevelUp[all_HeadInventory[_slotIndex].currentLevel]){
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

        if(DataManager.instance.coins >= all_HeadInventory[_slotIndex].requireCoinsToUpgrade)
        {
            return true;
        }

        return false;
    }


    public float SetHealthPer(int _slotIndex)
    {
        float perHealth = all_HeadInventory[_slotIndex].currentHealth * (all_HeadInventory[_slotIndex].healthIncrease / 100f);
        return perHealth;
    }

    public void UpgradeEquipnent(int _itemIndex)
    {
        currentMaterialCount -= all_HeadInventory[_itemIndex].requireMaterialToLevelUp[all_HeadInventory[_itemIndex].currentLevel];
        DataManager.instance.coins -= all_HeadInventory[_itemIndex].requireCoinsToUpgrade;
        all_HeadInventory[_itemIndex].currentHealth += all_HeadInventory[_itemIndex].healthIncrease;
        all_HeadInventory[_itemIndex].currentLevel++;
        DataManager.instance.SetHeadEQLevel(_itemIndex, all_HeadInventory[_itemIndex].currentLevel);
        DataManager.instance.SetHeadCurrentMat(currentMaterialCount);
    }
}
