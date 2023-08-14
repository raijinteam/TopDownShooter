using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotArrmorManager : MonoBehaviour
{

    public static SlotArrmorManager instance;


    public ArrmorEquipmentProperty[] all_ArrmorInventoryItems;
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
        DataManager.instance.SetArrmorCurrentMat(value);
        for (int i = 0; i < all_ArrmorInventoryItems.Length; i++)
        {
            DataManager.instance.SetArrmorEQLevel(i, value);
        }
        GetEQData();
    }

    public void GetEQData()
    {
        currentMaterialCount = DataManager.instance.GetArrmorMaterials();
        for (int i = 0; i < all_ArrmorInventoryItems.Length; i++)
        {
            all_ArrmorInventoryItems[i].currentLevel = DataManager.instance.GetArrmorCurrentLevel(i);
        }
    }


    public bool hasEnoughMaterialsForUpgrade(int _slotIndex)
    {
        if (currentMaterialCount >= all_ArrmorInventoryItems[_slotIndex].requireMaterialToLevelUp[all_ArrmorInventoryItems[_slotIndex].currentLevel])
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

        if (DataManager.instance.coins >= all_ArrmorInventoryItems[_slotIndex].requireCoinsToUpgrade)
        {
            return true;
        }

        return false;
    }


    public void UpgradeEquipnent(int _itemIndex)
    {
        currentMaterialCount -= all_ArrmorInventoryItems[_itemIndex].requireMaterialToLevelUp[all_ArrmorInventoryItems[_itemIndex].currentLevel];
        DataManager.instance.coins -= all_ArrmorInventoryItems[_itemIndex].requireCoinsToUpgrade;
        all_ArrmorInventoryItems[_itemIndex].currentHealth += all_ArrmorInventoryItems[_itemIndex].healthIncrease;
        all_ArrmorInventoryItems[_itemIndex].currentLevel++;
    }

}
