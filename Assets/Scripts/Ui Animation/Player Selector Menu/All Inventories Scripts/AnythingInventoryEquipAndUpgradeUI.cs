using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class AnythingInventoryEquipAndUpgradeUI : MonoBehaviour
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
        if (SlotAnythingManager.instance.all_AnythingInventoryItems[currentItemSelectedIndex].currentLevel == SlotGlovesManager.instance.maxLevel)
        {
            print("Disable update");
            btn_Upgrade.gameObject.SetActive(false);
            UiManager.instance.ui_PlayerManager.ui_EquipmentSlots.CheckIfUpgradeAvailableForEquippedAnythingItem();
        }


        img_EquipmentIcon.sprite = SlotAnythingManager.instance.all_AnythingInventoryItems[_itemIndex].sprite;
        txt_EquipmentName.text = SlotAnythingManager.instance.all_AnythingInventoryItems[_itemIndex].name;
        txt_EquipmentCurrentLevel.text = SlotAnythingManager.instance.all_AnythingInventoryItems[_itemIndex].currentLevel.ToString();
        txt_EquipmentMaxLevel.text = SlotAnythingManager.instance.maxLevel.ToString();
        txt_EquipmentCurrentValue.text = SlotAnythingManager.instance.all_AnythingInventoryItems[_itemIndex].currentFirerate.ToString();
        txt_EquipmentIncreaseValue.text = SlotAnythingManager.instance.all_AnythingInventoryItems[_itemIndex].firerateIncrease.ToString();

        txt_EquipmentcurrentMaterial.text = SlotAnythingManager.instance.currentMaterialCount.ToString();
        txt_EquipmentRequireMaterial.text = SlotAnythingManager.instance.all_AnythingInventoryItems[_itemIndex]
            .requireMaterialToLevelUp[SlotAnythingManager.instance.all_AnythingInventoryItems[_itemIndex].currentLevel].ToString();



    }

    public void OnClick_Equip()
    {
        PlayerSlotManager.instance.isAnythingItemEquipped = true;

        SlotAnythingManager.instance.currentEquippmentSelectedIndex = currentItemSelectedIndex;
        UiManager.instance.ui_PlayerManager.ui_EquipmentSlots.Assign_AnythingEquippedItem();


        UiManager.instance.ui_PlayerManager.SetAnythingSlotState();
       


        this.gameObject.SetActive(false);
    }


    public void OnClick_Upgrade()
    {
        if (!SlotAnythingManager.instance.hasEnoughMaterialsForUpgrade(currentItemSelectedIndex))
        {
            print("Not enough materials");
            return; // not enough materials
        }

        // check for coin, if not enough. Return;
        if (!SlotAnythingManager.instance.hasEnoughCoinsForUpgrade(currentItemSelectedIndex))
        {
            print("Not enough Coins");
            return; // not enough coins
        }


        SlotAnythingManager.instance.UpgradeEquipnent(currentItemSelectedIndex);

        // slot head manager increase level
        SetHeadEquipAndUpgradePanel(currentItemSelectedIndex);
        UiManager.instance.ui_PlayerManager.ui_EquipmentSlots.GetAnythingSlotLevelText().text = SlotGlovesManager.instance.all_GlovesInventoryItems[currentItemSelectedIndex].currentLevel.ToString();

    }


    public void OnClick_CloseButton()
    {
        this.gameObject.SetActive(false);
    }
}

