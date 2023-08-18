using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class HeadInventoryUI : MonoBehaviour
{
    [SerializeField] private HeadInventoryEquipAndUpgradeUI headInventoryEquipAndUpgradeUI; // HeadInventoryEquipAndUpgradeUI

    [SerializeField] private EquipmentPrefabData pf_InventoryButton;   
    [SerializeField] private Transform inventoryItemParent; // inventoryItemParent

    private void OnEnable()
    {

        for (int i = 0; i < SlotHeadEquipmentManager.instance.all_HeadInventory.Length; i++)
        {
            if (!SlotHeadEquipmentManager.instance.all_HeadInventory[i].isLocked)
            {
                EquipmentPrefabData obj = Instantiate(pf_InventoryButton, transform.position, Quaternion.identity, inventoryItemParent);

                if (i == SlotHeadEquipmentManager.instance.currentEquippmentSelectedIndex)
                    obj.img_SelectedBG.gameObject.SetActive(true);
                else
                    obj.img_SelectedBG.gameObject.SetActive(false);


                obj.img_EquipmentIcon.sprite = SlotHeadEquipmentManager.instance.all_HeadInventory[i].sprite;
                obj.txt_EquipmentCurrentLevel.text = SlotHeadEquipmentManager.instance.all_HeadInventory[i].currentLevel.ToString();
                int index = i; // test this with only i
                obj.GetComponent<Button>().onClick.AddListener(() => OnClick_Object(index));
            }
        }
    }

    public void OnClick_Object(int index)
    {
        headInventoryEquipAndUpgradeUI.gameObject.SetActive(true);
        headInventoryEquipAndUpgradeUI.SetHeadEquipAndUpgradePanel(index); 
    }

    private void OnDisable()
    {
        int childCount = inventoryItemParent.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(inventoryItemParent.transform.GetChild(i).gameObject);
        }
    }

    public void OnClick_CloseButton()
    { 
        this.gameObject.SetActive(false);
    }
}
