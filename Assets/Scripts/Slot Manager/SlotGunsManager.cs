using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotGunsManager : MonoBehaviour
{
    public static SlotGunsManager instance;


    public GunEquipmentProperty[] all_GunInventoryItems;
    public int currentMaterialCount;
    public int activeIndex;
    public int currentEquippmentSelectedIndex;

    public int maxLevel;
    private void Awake()
    {
        instance = this;
    }


    public void SetEqData(int value)
    {
        DataManager.instance.SetGunCurrentMat(value);
        for (int i = 0; i < all_GunInventoryItems.Length; i++)
        {
            DataManager.instance.SetGunEQLevel(i, value);
        }
        GetEQData();
    }


    public void IncreaseMaterial(int amount)
    {
        currentMaterialCount += amount;
        Debug.Log("Increase In Gun");
        DataManager.instance.SetGunCurrentMat(currentMaterialCount);
    }

    public void GetEQData()
    {
        currentMaterialCount = DataManager.instance.GetGunMaterials();
        for (int i = 0; i < all_GunInventoryItems.Length; i++)
        {
            all_GunInventoryItems[i].currentLevel = DataManager.instance.GetGunCurrentLevel(i);
        }
    }



    public bool hasEnoughMaterialsForUpgrade(int _slotIndex)
    {
        if (currentMaterialCount >= all_GunInventoryItems[_slotIndex].requireMaterialToLevelUp[all_GunInventoryItems[_slotIndex].currentLevel])
        {
            return true;
        }

        return false;
    }

    public bool hasEnoughCoinsForUpgrade(int _slotIndex)
    {
        int currentLevel = all_GunInventoryItems[_slotIndex].currentLevel;
        if (DataManager.instance.coins >= all_GunInventoryItems[_slotIndex].requireCoinsToUpgrade[currentLevel])
        {
            return true;
        }

        return false;
    }

   /* public float SetDamagePer(int _slotIndex)
    {
        float perDamage = all_GunInventoryItems[_slotIndex].currentDamage * (all_GunInventoryItems[_slotIndex].damageIncrease / 100f);
        return perDamage;
    }*/



    public void UpgradeEquipnent(int _itemIndex)
    {
        int currentLevel = all_GunInventoryItems[_itemIndex].currentLevel;
        currentMaterialCount -= all_GunInventoryItems[_itemIndex].requireMaterialToLevelUp[all_GunInventoryItems[_itemIndex].currentLevel];
        DataManager.instance.DecreaseCoins(all_GunInventoryItems[_itemIndex].requireCoinsToUpgrade[currentLevel]);
        all_GunInventoryItems[_itemIndex].currentLevel++;
        DataManager.instance.SetGunEQLevel(_itemIndex, all_GunInventoryItems[_itemIndex].currentLevel);
    }
}
