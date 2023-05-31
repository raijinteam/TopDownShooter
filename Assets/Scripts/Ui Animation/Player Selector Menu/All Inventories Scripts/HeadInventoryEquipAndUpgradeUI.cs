using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HeadInventoryEquipAndUpgradeUI : MonoBehaviour
{

    private int currentItemSelectedIndex;


    public Image img_EquipmentIcon;
    public TextMeshProUGUI txt_EquipmentName;
    public TextMeshProUGUI txt_EquipmentCurrentLevel;
    public TextMeshProUGUI txt_EquipmentMaxLevel;
    public TextMeshProUGUI txt_EquipmentCurrentValue;
    public TextMeshProUGUI txt_EquipmentIncreaseValue;
    public Button btn_Upgrade;


    [Header("Materials Property")]
    [SerializeField] private Image img_EquipmentMaterialIcon;
    public TextMeshProUGUI txt_EquipmentcurrentMaterial;
    public TextMeshProUGUI txt_EquipmentRequireMaterial;


    public void SetHeadEquipAndUpgradePanel(int _itemIndex)
    {
        currentItemSelectedIndex = _itemIndex;

        btn_Upgrade.gameObject.SetActive(true);
        //when Reach Full level
        if (SlotHeadEquipmentManager.instance.all_HeadInventory[currentItemSelectedIndex].currentLevel == SlotHeadEquipmentManager.instance.maxLevel)
        {
            print("Disable update");
            btn_Upgrade.gameObject.SetActive(false);
            UiManager.instance.ui_PlayerManager.ui_EquipmentSlots.CheckIfUpgradeAvailableForEquippedHeadItem();
        }


        img_EquipmentIcon.sprite = SlotHeadEquipmentManager.instance.all_HeadInventory[_itemIndex].sprite;
        txt_EquipmentName.text = SlotHeadEquipmentManager.instance.all_HeadInventory[_itemIndex].name;
        txt_EquipmentCurrentLevel.text = SlotHeadEquipmentManager.instance.all_HeadInventory[_itemIndex].currentLevel.ToString();
        txt_EquipmentMaxLevel.text = SlotHeadEquipmentManager.instance.maxLevel.ToString();
        txt_EquipmentCurrentValue.text = SlotHeadEquipmentManager.instance.all_HeadInventory[_itemIndex].currentHealth.ToString();
        txt_EquipmentIncreaseValue.text = SlotHeadEquipmentManager.instance.all_HeadInventory[_itemIndex].healthIncrease.ToString();

        txt_EquipmentcurrentMaterial.text = SlotHeadEquipmentManager.instance.currentMaterialCount.ToString();
        txt_EquipmentRequireMaterial.text = SlotHeadEquipmentManager.instance.all_HeadInventory[_itemIndex]
            .requireMaterialToLevelUp[SlotHeadEquipmentManager.instance.all_HeadInventory[_itemIndex].currentLevel].ToString();



    }

    public void OnClick_Equip()
    {
        PlayerSlotManager.instance.isHeadItemEquipped = true;


        SlotHeadEquipmentManager.instance.currentEquippmentSelectedIndex = currentItemSelectedIndex;

        UiManager.instance.ui_PlayerManager.ui_EquipmentSlots.Assign_HeadEquippedItem();
        UiManager.instance.ui_PlayerManager.SetHeadState();

        
       

        this.gameObject.SetActive(false);
    }


    public void OnClick_Upgrade()
    {
        if (!SlotHeadEquipmentManager.instance.hasEnoughMaterialsForUpgrade(currentItemSelectedIndex))
        {
            print("Not enough materials");
            return; // not enough materials
        }

        // check for coin, if not enough. Return;
        if (!SlotHeadEquipmentManager.instance.hasEnoughCoinsForUpgrade(currentItemSelectedIndex))
        {
            print("Not enough Coins");
            return; // not enough coins
        }


        SlotHeadEquipmentManager.instance.UpgradeEquipnent(currentItemSelectedIndex);
       


        // slot head manager increase level
        SetHeadEquipAndUpgradePanel(currentItemSelectedIndex);
        UiManager.instance.ui_PlayerManager.ui_EquipmentSlots.GetHeadSlotLevelText().text = SlotHeadEquipmentManager.instance.all_HeadInventory[currentItemSelectedIndex].currentLevel.ToString();

    }


    public void OnClick_CloseButton()
    {
        this.gameObject.SetActive(false);
    }
}
