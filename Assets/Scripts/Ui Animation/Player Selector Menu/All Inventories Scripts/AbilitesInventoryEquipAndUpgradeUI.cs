using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilitesInventoryEquipAndUpgradeUI : MonoBehaviour
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
        if (SlotAblitiesManager.instance.all_AbilitesInventoryItems[currentItemSelectedIndex].currentLevel == SlotAblitiesManager.instance.maxLevel)
        {
            print("Disable update");
            btn_Upgrade.gameObject.SetActive(false);
            UiManager.instance.ui_PlayerManager.ui_EquipmentSlots.CheckIfUpgradeAvailableForEquippedAbilitiesItem();
        }


        img_EquipmentIcon.sprite = SlotAblitiesManager.instance.all_AbilitesInventoryItems[_itemIndex].sprite;
        txt_EquipmentName.text = SlotAblitiesManager.instance.all_AbilitesInventoryItems[_itemIndex].name;
        txt_EquipmentCurrentLevel.text = SlotAblitiesManager.instance.all_AbilitesInventoryItems[_itemIndex].currentLevel.ToString();
        txt_EquipmentMaxLevel.text = SlotAblitiesManager.instance.maxLevel.ToString();
        txt_EquipmentCurrentValue.text = SlotAblitiesManager.instance.all_AbilitesInventoryItems[_itemIndex].currentFirerate.ToString();
        txt_EquipmentIncreaseValue.text = SlotAblitiesManager.instance.all_AbilitesInventoryItems[_itemIndex].firerateIncrease.ToString();

        txt_EquipmentcurrentMaterial.text = SlotAblitiesManager.instance.currentMaterialCount.ToString();
        txt_EquipmentRequireMaterial.text = SlotAblitiesManager.instance.all_AbilitesInventoryItems[_itemIndex]
            .requireMaterialToLevelUp[SlotAblitiesManager.instance.all_AbilitesInventoryItems[_itemIndex].currentLevel].ToString();



    }

    public void OnClick_Equip()
    {
        PlayerSlotManager.instance.isAblitiesItemEquipped = true;

        SlotAblitiesManager.instance.currentEquippmentSelectedIndex = currentItemSelectedIndex;
        UiManager.instance.ui_PlayerManager.ui_EquipmentSlots.Assign_AbilitiesEquippedItem();

        UiManager.instance.ui_PlayerManager.SetAbilitiesSlotState();

        


        this.gameObject.SetActive(false);
    }


    public void OnClick_Upgrade()
    {
        if (!SlotAblitiesManager.instance.hasEnoughMaterialsForUpgrade(currentItemSelectedIndex))
        {
            print("Not enough materials");
            return; // not enough materials
        }

        // check for coin, if not enough. Return;
        if (!SlotAblitiesManager.instance.hasEnoughCoinsForUpgrade(currentItemSelectedIndex))
        {
            print("Not enough Coins");
            return; // not enough coins
        }


        SlotAblitiesManager.instance.UpgradeEquipnent(currentItemSelectedIndex);


        // slot head manager increase level
        SetHeadEquipAndUpgradePanel(currentItemSelectedIndex);
        UiManager.instance.ui_PlayerManager.ui_EquipmentSlots.GetAbilitiesSlotLevelText().text = SlotAblitiesManager.instance.all_AbilitesInventoryItems[currentItemSelectedIndex].currentLevel.ToString();

    }


    public void OnClick_CloseButton()
    {
        this.gameObject.SetActive(false);
    }
}
