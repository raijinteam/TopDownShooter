using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentsSlotUI : MonoBehaviour
{
    public bool isHeadSlotEmpty;
    public RectTransform slot_Empty;
    public RectTransform slot_Fill;
    public Image img_ItemAvailable;

    public Image img_Icon;
    public TextMeshProUGUI txt_ItemLevel;
    public Image img_UpgradeAvailable;


    private void OnEnable()
    {
        //CHECK HEAD ITEM EQUPPED
        if (PlayerSlotManager.instance.isHeadItemEquipped)
        {
            Debug.Log("First This method called");
            Assign_HeadEquippedItem(); // AssignEquippedItem();
            img_ItemAvailable.gameObject.SetActive(false);
            isHeadSlotEmpty = false;
        }
        else
        {
            CheckIfWeHaveAnyHeadItemAvailable(); // CheckIfWeHaveAnyHeadItemAvailable
            isHeadSlotEmpty = true;
            slot_Fill.gameObject.SetActive(false);
            slot_Empty.gameObject.SetActive(true);
        }
    }


    public void Assign_HeadEquippedItem()
    {
        img_Icon.sprite = SlotHeadEquipmentManager.instance.all_HeadInventory[SlotHeadEquipmentManager.instance.currentEquippmentSelectedIndex].sprite;
        txt_ItemLevel.text = SlotHeadEquipmentManager.instance.all_HeadInventory[SlotHeadEquipmentManager.instance.currentEquippmentSelectedIndex]
            .currentLevel.ToString();

        ////SET SLOT LEVEL DATA WHEN LEVEL IS GETHER THEN 1
        //SlotHeadEquipmentManager.instance.SetLevelData();

        // Check For Updgrade
        CheckIfUpgradeAvailableForEquippedHeadItem();
    }

    public void CheckIfUpgradeAvailableForEquippedHeadItem()
    {
        // IF CURRENT ITEM LEVEL IS GREATER OR EQUAL TO MAX LEVEL


        if (!SlotHeadEquipmentManager.instance.hasEnoughMaterialsForUpgrade(SlotHeadEquipmentManager.instance.currentEquippmentSelectedIndex) ||
            (SlotHeadEquipmentManager.instance.all_HeadInventory[SlotHeadEquipmentManager.instance.currentEquippmentSelectedIndex].currentLevel == SlotHeadEquipmentManager.instance.maxLevel))
        {
            img_UpgradeAvailable.gameObject.SetActive(false);
        }
        else
        {
            img_UpgradeAvailable.gameObject.SetActive(true);
        }
    }


    public void CheckIfWeHaveAnyHeadItemAvailable()
    {
        slot_Empty.gameObject.SetActive(true);
        img_ItemAvailable.gameObject.SetActive(false);

        for (int i = 0; i < SlotHeadEquipmentManager.instance.all_HeadInventory.Length; i++)
        {
            if (!SlotHeadEquipmentManager.instance.all_HeadInventory[i].isLocked)
            {
                img_ItemAvailable.gameObject.SetActive(true);
                break;
            }
        }
    }


}
